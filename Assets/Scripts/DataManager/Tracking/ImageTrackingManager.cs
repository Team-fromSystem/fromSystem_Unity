using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImageTrackingManager
{
    public int imageID;
    public int modelID;
    public float modelSize;
    public PositionManager modelPosition;
    public RotationManager modelRotation;

    public ImageTrackingManager(int imageID,int modelID,float modelSize,PositionManager modelPosition,RotationManager modelRotation){
        this.imageID=imageID;
        this.modelID=modelID;
        this.modelSize=modelSize;
        this.modelPosition=modelPosition;
        this.modelRotation=modelRotation;
    }
}
