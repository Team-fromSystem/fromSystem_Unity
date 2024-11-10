using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Immersal.XR;
using Immersal.REST;
using Immersal;
public class ModelController : MonoBehaviour
{
    [SerializeField] private ModelData modelData;
    [SerializeField] private ImmersalData immersalData;
    [SerializeField] private PlaneTrackingData planeTrackingData;
    [SerializeField] private ImageTrackingData imageTrackingData;
    [SerializeField] private ImageData imageData;
    [SerializeField] private bool checkedXRMap = false;
    [SerializeField] private List<ModelManager> targetModels;
    [SerializeField] private List<ImmersalModelManager> targetModelsManagers;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject xrMap;
    private ImmersalSDK m_Sdk;
    [SerializeField] private GameObject children;
    private bool activeSW = false;
    // Update is called once per frame
    void Awake()
    {
        m_Sdk = ImmersalSDK.Instance;
        foreach (var modelManager in immersalData.chosenImmersalManager.immersalModelManager)
        {
            targetModelsManagers.Add(modelManager);
            targetModels.Add(modelData.modelManagers.Find(x => x.modelID == modelManager.modelID));
        }
        children = new GameObject("Children");
        children.SetActive(false);
        foreach (var targetModel in targetModels)
        {
            var targetModelManager = targetModelsManagers.Find(x => x.modelID == targetModel.modelID);
            PositionManager positionData = targetModelManager.modelPosition;
            RotationManager rotationData = targetModelManager.modelRotation;
            var mainModel = Instantiate(targetModel.model, new Vector3(positionData.X, positionData.Y, positionData.Z), Quaternion.Euler(rotationData.X, rotationData.Y, rotationData.Z), children.transform);
            var child = mainModel.transform;
            var size = immersalData.chosenImmersalManager.immersalModelManager[0].modelSize;
            child.localScale = Vector3.one * size;
        }

    }
    void Update()
    {
        int q = m_Sdk.TrackingStatus?.TrackingQuality ?? 0;
        if (checkedXRMap)
        {
            switch (q)
            {
                case 0:
                    // if (activeSW)
                    // {
                    // children.transform.SetParent(xrMap.transform);
                    children.SetActive(false);
                    //     activeSW = false;
                    // }
                    break;
                case 1:
                    // if (activeSW)
                    // {
                    // children.transform.SetParent(xrMap.transform);
                    children.SetActive(false);
                    //     activeSW = false;
                    // }
                    break;
                case 2:
                    // if (!activeSW)
                    // {
                    // children.transform.parent = null;
                    children.SetActive(true);
                    activeSW = true;
                    // }
                    break;
                default:
                    // if (!activeSW)
                    // {
                    // children.transform.parent = null;
                    children.SetActive(true);
                    activeSW = true;
                    // }
                    break;
            }
            return;
        }
        xrMap = GameObject.FindWithTag("XRMap");
        if (xrMap == null)
        {
            return;
        }
        var parent = xrMap.transform;
        children.transform.SetParent(parent);

        // child.SetParent(parent);
        checkedXRMap = true;
    }
}
