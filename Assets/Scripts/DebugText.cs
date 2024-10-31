// using Google.XR.ARCoreExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DebugText : MonoBehaviour
{
    // [SerializeField] AREarthManager earthManager;
    // [SerializeField] VpsStarter vpsStarter;
    TextMeshProUGUI debugText;
    [SerializeField] private MainSceneStarter mainSceneStarter;

    // [SerializeField] LocalizeChecker localizeChecker;

    // [SerializeField] CreateObject createObject;

    void Awake()
    {
        debugText = GetComponent<TextMeshProUGUI>();

    }

    void Update(){
        debugText.text=$"{mainSceneStarter.onTracked}";
    }

    // void Update()
    // {
    //     if (vpsStarter.availability != VpsAvailability.Available)
    //     {
    //         debugText.text = $"{vpsStarter.availability}";
    //         // string.Empty;
    //         return;
    //     }

    //     UpdateDebugInfo(debugText);
    // }

    // private void UpdateDebugInfo(TextMeshProUGUI debugText)
    // {
    //     var pose = earthManager.EarthState == EarthState.Enabled &&
    //         earthManager.EarthTrackingState == TrackingState.Tracking ?
    //         earthManager.CameraGeospatialPose : new GeospatialPose();

    //     debugText.text =
    //         $"SessionState: {ARSession.state}\n" +
    //         $"LocationServiceStatus: {Input.location.status}\n" +
    //         $"EarthState: {earthManager.EarthState}\n" +
    //         $"EarthTrackingState: {earthManager.EarthTrackingState}\n" +
    //         $"  LAT/LNG/ALT: {pose.Latitude:F6}, {pose.Longitude:F6}, {pose.Altitude:F2}\n" +
    //         $"  EunRotation: {pose.EunRotation:F2}\n" +
    //         $"  HorizontalAcc: {pose.HorizontalAccuracy:F6}\n" +
    //         $"  VerticalAcc: {pose.VerticalAccuracy:F2}\n" +
    //         $"  OrientationYawAcc: {pose.OrientationYawAccuracy:F2}\n" +
    //         $"  AnchorCount: {createObject.anchors.Count}\n" +
    //         $"  AnchorCount: {createObject.instances.Count}\n" +
    //         $"  AnchorCount: {createObject.text}\n" +
    //         $"  OnLocalize: {localizeChecker.isLocalizing}";
    // }
}