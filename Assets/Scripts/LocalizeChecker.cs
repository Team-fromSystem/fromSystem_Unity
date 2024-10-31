// using Google.XR.ARCoreExtensions;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LocalizeChecker : MonoBehaviour
{
    // public double orientationYawAccuracyThreshold = 25;
    // public double horizontalAccuracyThreshold = 20;

    // public Action OnFinishedLocalization { get; set; }
    // public Action OnLostLocalization { get; set; }

    // [SerializeField] AREarthManager earthManager;
    // [SerializeField] VpsStarter vpsStarter;

    // public bool isLocalizing = true;

    // void Update()
    // {
    //     if (vpsStarter.availability != VpsAvailability.Available) return;

    //     if (IsLocalizing())
    //     {
    //         // Lost localization during the session.
    //         if (!isLocalizing)
    //         {
    //             OnLostLocalization?.Invoke();
    //             isLocalizing = true;
    //         }
    //     }
    //     else if (isLocalizing)
    //     {
    //         // Finished localization.
    //         OnFinishedLocalization?.Invoke();
    //         isLocalizing = false;
    //     }
    // }

    // bool IsLocalizing()
    // {
    //     bool isSessionReady = ARSession.state == ARSessionState.SessionTracking &&
    //         Input.location.status == LocationServiceStatus.Running;
    //     var earthTrackingState = earthManager.EarthTrackingState;
    //     var pose = earthTrackingState == TrackingState.Tracking ?
    //         earthManager.CameraGeospatialPose : new GeospatialPose();

    //     if (!isSessionReady ||
    //         earthTrackingState != TrackingState.Tracking ||
    //         pose.OrientationYawAccuracy > orientationYawAccuracyThreshold ||
    //         pose.HorizontalAccuracy > horizontalAccuracyThreshold)
    //     {
    //         return true;
    //     }

    //     return false;
    // }
}