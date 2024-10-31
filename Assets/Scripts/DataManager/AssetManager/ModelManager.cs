using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelManager
{
    public int modelID;
    public GameObject model;

    public ModelManager(int modelID,GameObject model){
        this.modelID=modelID;
        this.model=model;
    }
}
