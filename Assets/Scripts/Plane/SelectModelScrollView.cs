using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(LoopScrollRect))]
[DisallowMultipleComponent]
public sealed class SelectModelScrollView : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ModelData modelData;
    [SerializeField] private bool isEvent = false;
    [SerializeField] private bool isMM = false;
    [SerializeField] private PlaneTrackingData planeTrackingData;
    private List<ModelManager> eventModelList = new List<ModelManager>();

    public int totalCount = -1;
    private ObjectPool<GameObject> _pool;

    void Awake()
    {
        if (!isEvent)
        {
            return;
        }
        if (isMM)
        {
            foreach (var id in planeTrackingData.planeTrackingManager.mainModelID)
            {
                var oneData = modelData.modelManagers.Find((x) => x.modelID == id);
                eventModelList.Add(oneData);
            }
        }
        else
        {
            foreach (var id in planeTrackingData.planeTrackingManager.decorationModelID)
            {
                var oneData = modelData.modelManagers.Find((x) => x.modelID == id);
                eventModelList.Add(oneData);
            }
        }
    }

    private void Start()
    {
        // オブジェクトプールを作成
        _pool = new ObjectPool<GameObject>(
            // オブジェクト生成処理
            () => Instantiate(_prefab),
            // オブジェクトがプールから取得される時の処理
            o => o.SetActive(true),
            // オブジェクトがプールに戻される時の処理
            o =>
            {
                o.transform.SetParent(transform);
                o.SetActive(false);
            });

        var scrollRect = GetComponent<LoopScrollRect>();
        scrollRect.prefabSource = this;
        scrollRect.dataSource = this;
        if (isEvent)
        {
            scrollRect.totalCount = eventModelList.Count;
        }
        else
        {
            scrollRect.totalCount = modelData.modelManagers.Count;
        }
        scrollRect.RefillCells();
    }

    void LoopScrollDataSource.ProvideData(Transform trans, int index)
    {
        if (isEvent)
        {
            trans.GetChild(0).GetComponent<TextMeshProUGUI>().text = eventModelList[index].modelName;
            trans.GetComponent<ModelListViewController>().thisData = eventModelList[index];
        }
        else
        {
            trans.GetChild(0).GetComponent<TextMeshProUGUI>().text = modelData.modelManagers[index].modelName;
            trans.GetComponent<ModelListViewController>().thisData = modelData.modelManagers[index];
        }
    }

    GameObject LoopScrollPrefabSource.GetObject(int index)
    {
        // オブジェクトプールからGameObjectを取得
        return _pool.Get();
    }

    void LoopScrollPrefabSource.ReturnObject(Transform trans)
    {
        // オブジェクトプールにGameObjectを返却
        _pool.Release(trans.gameObject);
    }
}