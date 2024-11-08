using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class PlaneCreateModel : MonoBehaviour
{
    public float m_ClickHoldTime = 0.1f;
    public float m_timeHold = 0f;
    ARPlaneManager aRPlaneManager;
    ARRaycastManager aRRaycastManager;

    public TMP_Dropdown mainModelDropdown;
    public TMP_Dropdown subModelDropdown;

    public List<GameObject> objectList = new List<GameObject>();

    private GameObject mainModelObject;

    public GameObject instantObject;


    public List<GameObject> itemObjectList = new List<GameObject>();


    [SerializeField] private GameObject mainModelDropdownObj;
    [SerializeField] private GameObject itemDropdownObj;
    [SerializeField] private GameObject clearButtonObj;
    [SerializeField] private GameObject screenShotButtonObj;
    [SerializeField] private GameObject addButtonObj;

    public ModelData modelData;
    public PlaneTrackingData planeTrackingData;
    [SerializeField] private ImageTrackingData imageTrackingData;
    [SerializeField] private ImageData imageData;

    public Decoration decoration;
    private bool setItemObject = false;
    private bool setMainObject = false;

    void Awake()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();
        aRRaycastManager = GetComponent<ARRaycastManager>();
        objectList.Clear();
        Debug.Log($"{planeTrackingData.planeTrackingManager.mainModelID.Count}");
        Debug.Log($"{modelData.modelManagers.Count}");
        foreach (var modelID in planeTrackingData.planeTrackingManager.mainModelID)
        {
            var model = modelData.modelManagers.Find(x => x.modelID == modelID);
            if (model == null)
            {
                return;
            }
            var setModel = model.model;
            setModel.AddComponent<DragObject>();
            if (!setMainObject)
            {
                mainModelObject = setModel;
                setMainObject = true;
            }
            objectList.Add(setModel);
            mainModelDropdown.options.Add(new TMP_Dropdown.OptionData($"{model.modelName}"));
        }
        foreach (var obj in objectList)
        {
            Debug.Log($"Main：{obj.name}");
        }
        mainModelDropdown.RefreshShownValue();
        Debug.Log("StartDecoSet");
        decoration.itemList.Clear();
        foreach (var submodelID in planeTrackingData.planeTrackingManager.decorationModelID)
        {
            var subModel = modelData.modelManagers.Find(x => x.modelID == submodelID);
            if (subModel == null)
            {
                return;
            }
            var setModel = subModel.model;
            setModel.AddComponent<DragObject>();
            if (!setItemObject)
            {
                decoration.ItemObject = setModel;
                setItemObject = true;
            }
            decoration.itemList.Add(setModel);
            subModelDropdown.options.Add(new TMP_Dropdown.OptionData($"{subModel.modelName}"));
        }
        foreach (var obj in decoration.itemList)
        {
            Debug.Log($"Decoration：{obj.name}");
        }
        subModelDropdown.RefreshShownValue();
    }

    public void ChangeMainModel()
    {
        mainModelObject = objectList[mainModelDropdown.value];
    }

    void Update()
    {
        if (mainModelObject == null)
        {
            Debug.Log("setMainModelObject");
            mainModelObject = objectList[mainModelDropdown.value];
            return;
        }
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended || instantObject != null)
        {
            return;
        }

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            return;
        }

        var hitList = new List<ARRaycastHit>();
        if (aRRaycastManager.Raycast(Input.GetTouch(0).position, hitList, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("生成");
            mainModelDropdownObj.SetActive(false);
            clearButtonObj.SetActive(true);
            itemDropdownObj.SetActive(true);
            addButtonObj.SetActive(true);
            screenShotButtonObj.SetActive(true);
            var hitPose = hitList[0].pose;
            GameObject obj = Instantiate(mainModelObject, hitPose.position, hitPose.rotation);
            mainModelObject = obj;
            instantObject = obj;
            // aRPlaneManager.planesChanged += OnPlanesChanged;
            aRPlaneManager.enabled = false;
            foreach (var plane in aRPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }

    public void Clear()
    {
        if (instantObject != null)
        {
            aRPlaneManager.enabled = true;
            // aRPlaneManager.planesChanged -= OnPlanesChanged;
            foreach (var plane in aRPlaneManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }
            foreach (var item in itemObjectList)
            {
                Destroy(item);
            }
            Destroy(instantObject);
            instantObject = null;
            mainModelDropdownObj.SetActive(true);
            clearButtonObj.SetActive(false);
            itemDropdownObj.SetActive(false);
            addButtonObj.SetActive(false);
            screenShotButtonObj.SetActive(false);
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
