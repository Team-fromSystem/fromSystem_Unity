using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImageManager
{
    public int imageID;
    public Texture2D image;

    public ImageManager(int imageID, Texture2D image){
        this.imageID=imageID;
        this.image=image;
    }
}
