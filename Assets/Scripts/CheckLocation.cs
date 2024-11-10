using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CheckLocation : MonoBehaviour
{
    [SerializeField] private double earthRX = 6378.137;
    [SerializeField] private double earthRY = 6356.752;
    [SerializeField] private LocationManager targetLocation;

    [SerializeField] private double distance;
    [SerializeField] private double compensationRadius;

    [SerializeField] private GameObject actionButton;
    [SerializeField] private ImmersalData immersalData;
    [SerializeField] private ImmersalManager nowTargetData;

    public double Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
        }
    }

    [SerializeField] private string locationState;

    public string LocationState
    {
        get
        {
            return locationState;
        }
        set
        {
            locationState = value;
        }
    }

    [SerializeField] private string targetLocationText;

    public string TargetLocationText
    {
        get
        {
            return targetLocationText;
        }
        set
        {
            targetLocationText = value;
        }
    }

    // // Update is called once per frame
    void Update()
    {
        if (!Location.Instance.LocationStatus)
        {
            return;
        }

        LocationManager currentLocation = new LocationManager(deg2rad(Location.Instance.latitude), deg2rad(Location.Instance.longitude), 0.0);
        if (nowTargetData != null)
        {
            LocationManager targetLocation = new LocationManager(deg2rad(nowTargetData.location.latitude), deg2rad(nowTargetData.location.longitude), 0.0);
            distance = Location_Distance(currentLocation, targetLocation);
            if (distance > nowTargetData.radius + compensationRadius)
            {
                nowTargetData = null;
                immersalData.chosenImmersalManager = null;
                actionButton.SetActive(false);
            }
            targetLocationText = $"targetLocation:{nowTargetData.location.latitude}";
            locationState = $"distance:{distance}";
            return;
        }
        foreach (var data in immersalData.immersalManagers)
        {
            targetLocationText = $"targetLocation:{data.location.latitude}";
            LocationManager targetLocation = new LocationManager(deg2rad(data.location.latitude), deg2rad(data.location.longitude), 0.0);
            distance = Location_Distance(currentLocation, targetLocation);
            if (distance <= data.radius)
            {
                actionButton.SetActive(true);
                nowTargetData = data;
                locationState = $"distance:{distance}";
                immersalData.chosenImmersalManager = nowTargetData;
                return;
            }
            locationState = "";
        }
    }
    double deg2rad(double deg)
    {
        return deg * Mathf.PI / 180.0;
    }

    double Location_Distance(LocationManager currentLocation, LocationManager targetLocation)
    {
        double p1 = Math.Atan(earthRY / earthRX * Math.Tan(currentLocation.latitude));
        double p2 = Math.Atan(earthRY / earthRX * Math.Tan(targetLocation.latitude));
        double X = Math.Acos(Math.Sin(p1) * Math.Sin(p2) + Math.Cos(p1) * Math.Cos(p2) * Math.Cos(currentLocation.longitude - targetLocation.longitude));
        double F = (earthRX - earthRY) / earthRX;
        double dr = F / 8 * ((Math.Sin(X) - X) * Math.Pow((Math.Sin(p1) + Math.Sin(p2)), 2.0) / Math.Pow(Math.Cos(X / 2), 2.0) - (Math.Sin(X) + X) * Math.Pow((Math.Sin(p1) - Math.Sin(p2)), 2.0) / Math.Pow(Math.Sin(X / 2), 2.0));
        return earthRX * (X + dr);
    }

    public void ChangeScene()
    {
        StartCoroutine(LoadNextSceneAsync("MapDownloadSample"));
    }

    private IEnumerator LoadNextSceneAsync(string sceneName)
    {
        // 次のシーンを非同期で読み込み
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }
}
