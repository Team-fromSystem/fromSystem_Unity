using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSingleton : Singleton<ImageSingleton>
{
    public List<ImageManager> imageData;
}
