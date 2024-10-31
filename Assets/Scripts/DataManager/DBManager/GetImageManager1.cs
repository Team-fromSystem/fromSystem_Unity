using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetImageManager
{
    public int imageID;
    public string fileLink;
    public string filePath;

    public GetImageManager(int imageID,string fileLink,string filePath){
        this.imageID=imageID;
        this.fileLink=fileLink;
        this.filePath=filePath;
    }
}
