using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImmersalModelManager
{
    public int modelID;
    public float modelSize;
    public PositionManager modelPosition;
    public RotationManager modelRotation;
    public ImmersalModelManager(int modelID,float modelSize,PositionManager modelPosition,RotationManager modelRotation){
        this.modelID=modelID;
        this.modelSize=modelSize;
        this.modelPosition=modelPosition;
        this.modelRotation=modelRotation;
    }
}
