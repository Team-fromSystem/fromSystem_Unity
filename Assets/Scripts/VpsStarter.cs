using System;
using System.Collections;
// using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class VpsStarter : MonoBehaviour
{
    // [NonSerialized] public VpsAvailability availability = VpsAvailability.Unknown;

    // bool waitingForLocationService = false;

    // void Start()
    // {
    //     StartCoroutine("StartLocationService");
    //     StartCoroutine("AvailabilityCheck");
    // }

    // IEnumerator StartLocationService()
    // {
    //     waitingForLocationService = true;

    //     if (!Input.location.isEnabledByUser)
    //     {
    //         Debug.Log("Location service is disabled by the user.");
    //         waitingForLocationService = false;
    //         yield break;
    //     }

    //     Debug.Log("Starting location service.");
    //     Input.location.Start();

    //     while (Input.location.status == LocationServiceStatus.Initializing)
    //     {
    //         yield return null;
    //     }

    //     waitingForLocationService = false;
    //     if (Input.location.status != LocationServiceStatus.Running)
    //     {
    //         Debug.LogWarning($"Location service ended with {Input.location.status} status.");
    //         Input.location.Stop();
    //     }
    // }

    // IEnumerator AvailabilityCheck()
    // {
    //     if (ARSession.state == ARSessionState.None)
    //     {
    //         yield return ARSession.CheckAvailability();
    //     }

    //     // Waiting for ARSessionState.CheckingAvailability.
    //     yield return null;

    //     if (ARSession.state == ARSessionState.NeedsInstall)
    //     {
    //         yield return ARSession.Install();
    //     }

    //     // Waiting for ARSessionState.Installing.
    //     yield return null;

    //     while (waitingForLocationService)
    //     {
    //         yield return null;
    //     }

    //     if (Input.location.status != LocationServiceStatus.Running)
    //     {
    //         Debug.LogWarning("Location services aren't running. VPS availability check is not available.");
    //         yield break;
    //     }

    //     var location = Input.location.lastData;
    //     var vpsAvailabilityPromise = AREarthManager.CheckVpsAvailabilityAsync(location.latitude, location.longitude);
    //     yield return vpsAvailabilityPromise;

    //     availability = vpsAvailabilityPromise.Result;
    //     Debug.Log($"VPS Availability at ({location.latitude}, {location.longitude}): {vpsAvailabilityPromise.Result}");
    // }
}