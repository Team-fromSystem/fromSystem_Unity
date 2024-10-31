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
    public EventData eventData;
    ARPlaneManager aRPlaneManager;
    ARRaycastManager aRRaycastManager;

    public TMP_Dropdown mainModelDropdown;
    public TMP_Dropdown subModelDropdown;

    public List<GameObject> objectList = new List<GameObject>();

    private GameObject mainModelObject;

    public GameObject instantObject;


    public List<GameObject> itemObjectList = new List<GameObject>();

    public bool clickedButton = false;

    public Canvas mainModelDropdownCanvas;
    public Canvas clearButtonCanvas;

    public Canvas itemDropdownCanvas;
    public ModelData modelData;
    public PlaneTrackingData planeTrackingData;

    public Decoration decoration;
    private bool setItemObject = false;
    private bool setMainObject = false;

    void Awake()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();
        aRRaycastManager = GetComponent<ARRaycastManager>();
        clearButtonCanvas.enabled = false;
        itemDropdownCanvas.enabled = false;
        foreach (var modelID in planeTrackingData.planeTrackingManager.mainModelID)
        {
            var model = modelData.modelManagers.Find(x => x.modelID == modelID);
            if (model == null)
            {
                return;
            }
            if(!setMainObject){
                mainModelObject=model.model;
                setMainObject=true;
            }
            mainModelDropdown.options.Add(new TMP_Dropdown.OptionData($"{modelID}"));
        }
        mainModelDropdown.RefreshShownValue();
        foreach (var submodelID in planeTrackingData.planeTrackingManager.decorationModelID)
        {
            var subModel = modelData.modelManagers.Find(x => x.modelID == submodelID);
            if (subModel == null)
            {
                return;
            }
            if (!setItemObject)
            {
                decoration.ItemObject = subModel.model;
                setItemObject = true;
            }
            subModelDropdown.options.Add(new TMP_Dropdown.OptionData($"{submodelID}"));
        }
    }

    public void ChangeMainModel(){
        mainModelObject = objectList[mainModelDropdown.value];
    }

    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended || instantObject != null || clickedButton)
        {
            return;
        }

        if (mainModelObject == null)
        {
            return;
        }

        var hitList = new List<ARRaycastHit>();
        if (aRRaycastManager.Raycast(Input.GetTouch(0).position, hitList, TrackableType.PlaneWithinPolygon))
        {
            mainModelDropdownCanvas.enabled = false;
            clearButtonCanvas.enabled = true;
            itemDropdownCanvas.enabled = true;
            var hitPose = hitList[0].pose;
            var obj = Instantiate(mainModelObject, hitPose.position, hitPose.rotation);
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
        clickedButton = true;
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
            mainModelDropdownCanvas.enabled = true;
            clearButtonCanvas.enabled = false;
            itemDropdownCanvas.enabled = false;
        }
    }

    public void notTap()
    {
        clickedButton = true;
    }

    public void allowTap()
    {
        StartCoroutine(ChengeState());
    }

    private IEnumerator ChengeState()
    {
        yield return new WaitForSeconds(0.1f);
        clickedButton = false;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            plane.gameObject.SetActive(false); // 新しく追加された平面を非アクティブにする
        }
    }
}
