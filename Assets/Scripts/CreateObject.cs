// using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using System.Collections.Generic;
using TMPro;

public class CreateObject : MonoBehaviour
{
    // public TMP_InputField latitude;
    // public TMP_InputField longitude;
    // public TMP_InputField altitude;

    // public GameObject prefab;

    // public List<ARGeospatialAnchor> anchors = new List<ARGeospatialAnchor>();
    // public List<GameObject> instances = new List<GameObject>();

    // public string text="None";

    // [SerializeField] LocalizeChecker localizeChecker;
    // [SerializeField] ARAnchorManager anchorManager;

    // void Start()
    // {
    //     latitude = latitude.GetComponent<TMP_InputField>();
    //     longitude = longitude.GetComponent<TMP_InputField>();
    //     altitude = altitude.GetComponent<TMP_InputField>();
    //     // localizeChecker.OnFinishedLocalization += SpawnObject;
    //     localizeChecker.OnLostLocalization += DestroyObject;
    // }

    // // void SpawnObject()
    // // {
    // //     Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
    // //     var anchor = anchorManager.AddAnchor(latitude, longitude, altitude, rotation);
    // //     instance = Instantiate(prefab, anchor.transform);
    // //     anchors.Add(anchor);
    // // }

    // public void OnClick()
    // {
    //     if (localizeChecker.isLocalizing)
    //     {
    //         double latitudeData;
    //         double longitudeData;
    //         double altitudeData;
    //         if (!(Double.TryParse(latitude.text, out latitudeData))) return;
    //         if (!(Double.TryParse(longitude.text, out longitudeData))) return;
    //         if (!(Double.TryParse(altitude.text, out altitudeData))) return;
    //         Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
    //         var anchor = anchorManager.AddAnchor(latitudeData, longitudeData, altitudeData, rotation);
    //         var instance = Instantiate(prefab, anchor.transform);
    //         anchors.Add(anchor);
    //         instances.Add(instance);
    //         text="Exit";
    //     }
    // }

    // void DestroyObject()
    // {
    //     if (instances.Count>0)
    //     {
    //         foreach(var instance in instances){
    //             Destroy(instance);
    //         }
    //         instances.Clear();
    //         text="Clear";
    //     }
    //     if (anchors.Count > 0)
    //     {
    //         foreach(var anchor in anchors){
    //             Destroy(anchor);
    //         }
    //         anchors.Clear();
    //     }
    // }
}
