using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocationManager
{

    public double latitude;
    public double longitude;
    public double altitude;

    public LocationManager(){
        latitude=0.0;
        longitude=0.0;
        altitude=0.0;
    }

    public LocationManager(double lat, double lon, double alt)
    {
        latitude=lat;
        longitude=lon;
        altitude=alt;
    }

}
