using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirestoreManager
{
    public List<object> modelID;
    public List<object> imageID;
    public List<int> detectType;

    public FirestoreManager(List<object> modelID,List<object> imageID,List<int> detectType){
        this.modelID=modelID;
        this.imageID=imageID;
        this.detectType=detectType;
    }
}
