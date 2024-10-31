using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GetModelManager
{
    public int modelID;
    public string fileLink;
    public string filePath;

    public GetModelManager(int modelID,string fileLink,string filePath){
        this.modelID=modelID;
        this.fileLink=fileLink;
        this.filePath=filePath;
    }
}
