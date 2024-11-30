using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class CreateModelController : MonoBehaviour
{
    [SerializeField] private ARPlaneManager aRPlaneManager;
    [SerializeField] private ARRaycastManager aRRaycastManager;

    public List<GameObject> objectList = new List<GameObject>();

    private GameObject chosedModelObject;

    public GameObject instantObject;


    public List<GameObject> itemObjectList = new List<GameObject>();

    public ModelData modelData;
    public PlaneTrackingData planeTrackingData;
    [SerializeField] private ImageTrackingData imageTrackingData;
    [SerializeField] private ImageData imageData;

    [SerializeField] private ChosedModelData chosedModelData;
    [SerializeField] private SettingModelController settingModelController;
    [SerializeField] private bool isSS = false;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas SSCanvas;

    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended||mainCanvas.enabled==false)
        {
            return;
        }
        Debug.Log("タッチ");

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            return;
        }
        Debug.Log("UI重なりなし");
        var hitList = new List<ARRaycastHit>();
        Debug.Log("raycast格納場所");
        if (aRRaycastManager.Raycast(Input.GetTouch(0).position, hitList, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("OK Raycast");
            if (chosedModelData.chosedData.model == null)
            {
                Debug.Log("モデルを選択して下さい");
                return;
            }
            Debug.Log("モデル取得");
            chosedModelObject = chosedModelData.chosedData.model;
            Debug.Log("生成");
            var hitPose = hitList[0].pose;
            Transform cameraTransform = Camera.main.transform;
            GameObject go = Instantiate(chosedModelObject, hitPose.position, hitPose.rotation);
            Vector3 direction = cameraTransform.position - go.transform.position;
            go.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
            var comp = go.AddComponent<ClickObject>();
            comp.settingModelController = this.settingModelController;
            go.AddComponent<ColliderVisualizer>();
            var DO = go.AddComponent<DraggingObject>();
            DO.settingModelController = this.settingModelController;
            objectList.Add(go);
            // aRPlaneManager.planesChanged += OnPlanesChanged;
            // aRPlaneManager.enabled = false;
            // foreach (var plane in aRPlaneManager.trackables)
            // {
            //     plane.gameObject.SetActive(false);
            // }
        }
    }

    public void AddContent()
    {
        if (chosedModelData.chosedData.model == null)
        {
            Debug.Log("モデルを選択して下さい");
            return;
        }
        chosedModelObject = chosedModelData.chosedData.model;
        Debug.Log("生成");
        Transform cameraTransform = Camera.main.transform;
        GameObject go = Instantiate(chosedModelObject, cameraTransform.position + cameraTransform.forward, Quaternion.identity);
        Vector3 direction = cameraTransform.position - go.transform.position;
        go.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        // GameObject go = Instantiate(chosedModelObject, new Vector3(660, 526, 6), Quaternion.identity);
        var comp = go.AddComponent<ClickObject>();
        comp.settingModelController = this.settingModelController;
        go.AddComponent<ColliderVisualizer>();
        var DO = go.AddComponent<DraggingObject>();
        DO.settingModelController = this.settingModelController;
        objectList.Add(go);
    }

    public void Clear(GameObject obj)
    {
        objectList.Remove(obj);
        Destroy(obj);
    }

    public void Reset()
    {
        if (objectList.Count > 0)
        {
            foreach (var obj in objectList)
            {
                Destroy(obj);
            }
            objectList.Clear();
        }
    }

    public void OpenSS()
    {
        mainCanvas.enabled = false;
        SSCanvas.enabled = true;
        aRPlaneManager.enabled = false;
        foreach (var plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        foreach (var go in objectList)
        {
            if (go.TryGetComponent<Collider>(out Collider col))
            {
                col.enabled = false;
            }
        }
    }
    public void CloseSS()
    {
        SSCanvas.enabled = false;
        mainCanvas.enabled = true;
        aRPlaneManager.enabled = true;
        foreach (var plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
        foreach (var go in objectList)
        {
            if (go.TryGetComponent<Collider>(out Collider col))
            {
                col.enabled = true;
            }
        }
    }
    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            plane.gameObject.SetActive(false); // 新しく追加された平面を非アクティブにする
        }
    }
}
