using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="TrackingModeData")]
public class TrackingModeData : ScriptableObject
{
    public bool useImageTracking;
    public bool useGeospatial;
    public bool usePlaneTracking;
    public bool useImmersal;
}
