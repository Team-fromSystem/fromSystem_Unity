using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImmersalMapManager
{
    public int mapID;
    public PositionManager mapPosition;
    public RotationManager mapRotation;
    public ImmersalMapManager(int mapID,PositionManager mapPosition,RotationManager mapRotation){
        this.mapID=mapID;
        this.mapPosition=mapPosition;
        this.mapRotation=mapRotation;
    }
}
