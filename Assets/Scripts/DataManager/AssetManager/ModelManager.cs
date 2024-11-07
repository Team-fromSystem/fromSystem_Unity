using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelManager
{
    public int modelID;
    public string modelName;
    public GameObject model;

    public ModelManager(int modelID,string modelName,GameObject model){
        this.modelID=modelID;
        this.modelName=modelName;
        this.model=model;
    }
}
