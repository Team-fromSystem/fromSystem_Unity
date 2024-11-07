using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckDistance : MonoBehaviour
{
    [SerializeField] private double earthRX = 6378.137;
    [SerializeField] private double earthRY = 6356.752;
    [SerializeField] private LocationManager targetLocation;

    [SerializeField] private double distance;
    [SerializeField] private double compensationRadius;

    [SerializeField] private GameObject popUp;
    [SerializeField] private ImmersalData immersalData;

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

    [SerializeField] private bool inTargetLocation = false;

    public bool IntargetLocation
    {
        get
        {
            return inTargetLocation;
        }
        set
        {
            inTargetLocation = value;
        }
    }


    void Awake()
    {
        targetLocation = new LocationManager(deg2rad(immersalData.chosenImmersalManager.location.latitude), deg2rad(immersalData.chosenImmersalManager.location.longitude), 0.0);
    }

    // // Update is called once per frame
    void Update()
    {
        if (!Location.Instance.LocationStatus)
        {
            return;
        }

        LocationManager currentLocation = new LocationManager(deg2rad(Location.Instance.latitude), deg2rad(Location.Instance.longitude), 0.0);
        distance = Location_Distance(currentLocation, targetLocation);
        if (distance > immersalData.chosenImmersalManager.radius + compensationRadius && inTargetLocation)
        {
            popUp.SetActive(true);
            inTargetLocation = false;
            locationState = $"distance:{distance}";
            return;
        }
        if (distance < immersalData.chosenImmersalManager.radius && !inTargetLocation)
        {
            popUp.SetActive(false);   
            inTargetLocation = true;
        }
        locationState = $"distance:{distance}";
        return;
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
}
