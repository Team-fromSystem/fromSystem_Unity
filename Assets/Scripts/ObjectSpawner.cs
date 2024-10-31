// using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    // public double latitude;
    // public double longitude;
    // public double altitude;

    // public GameObject parent;
    // public GameObject prefab;

    // List<ARGeospatialAnchor> anchors=new List<ARGeospatialAnchor>();
    // GameObject instance;

    // [SerializeField] LocalizeChecker localizeChecker;
    // [SerializeField] ARAnchorManager anchorManager;

    // void Start()
    // {
    //     localizeChecker.OnFinishedLocalization += SpawnObject;
    //     localizeChecker.OnLostLocalization += DestroyObject;
    // }

    // void SpawnObject()
    // {
    //     Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
    //     var anchor = anchorManager.AddAnchor(latitude, longitude, altitude, rotation);
    //     instance = Instantiate(prefab, anchor.transform);
    //     anchors.Add(anchor);
    // }

    // void DestroyObject()
    // {
    //     if (instance != null)
    //     {
    //         Destroy(instance);
    //         instance = null;
    //     }
    //     if (anchors?.Count > 0)
    //     {
    //         anchors.Clear();
    //     }
    // }
}