using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GetModelManager
{
    public int modelID;
    public int hostID;
    public string modelURL;
    public string modelName;
    public string fileFormat;
    public PositionManager colliderPosition;
    public float colliderRadius;

    public GetModelManager(int modelID,int hostID,string modelURL,string modelName,string fileFormat,PositionManager colliderPosition,float colliderRadius){
        this.modelID=modelID;
        this.hostID=hostID;
        this.modelURL=modelURL;
        this.modelName=modelName;
        this.fileFormat=fileFormat;
        this.colliderPosition=colliderPosition;
        this.colliderRadius=colliderRadius;
    }
}
