using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ImageTrackingData")]
public class ImageTrackingData : ScriptableObject
{
    public List<ImageTrackingManager> imageTrackingManagers;
}
