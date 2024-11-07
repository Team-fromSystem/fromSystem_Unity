using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GetModelManager
{
    public int modelID;
    public int hostID;
    public string modelURL;
    public string modelName;
    public string fileFormat;

    public GetModelManager(int modelID,int hostID,string modelURL,string modelName,string fileFormat){
        this.modelID=modelID;
        this.hostID=hostID;
        this.modelURL=modelURL;
        this.modelName=modelName;
        this.fileFormat=fileFormat;
    }
}
