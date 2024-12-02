using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetImageManager
{
    public int imageID;
    public int hostID;
    public string imageURL;
    public string imageName;
    public string fileFormat;

    public GetImageManager(int imageID,int hostID,string imageURL,string imageName,string fileFormat){
        this.imageID=imageID;
        this.hostID=hostID;
        this.imageURL=imageURL;
        this.imageName=imageName;
        this.fileFormat=fileFormat;
    }
}
