using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="EventData")]
public class EventData :ScriptableObject
{
    public GameObject planeObject;
    public List<Texture2D> image=new List<Texture2D>();

    public List<ImagePrefabs> imagePrefabsList=new List<ImagePrefabs>();

    public class ImagePrefabs{
        public string imageName;
        public GameObject imageObject;
    }

    public GameObject geoObject;

    public GameObject immersalObject;

    public int  mapID;
}