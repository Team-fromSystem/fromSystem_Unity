using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaneTrackingManager
{
    public List<int> mainModelID;
    public List<int> decorationModelID;

    public PlaneTrackingManager(List<int> mainModelID,List<int> decorationModelID){
        this.mainModelID=new List<int>(mainModelID);
        this.decorationModelID=new List<int>(decorationModelID);
    }
}
