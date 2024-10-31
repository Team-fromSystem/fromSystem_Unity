using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImmersalManager
{
    public LocationManager location;
    public float radius;
    public List<ImmersalMapManager> immersalMapManager;
    public List<ImmersalModelManager> immersalModelManager;

    public ImmersalManager(LocationManager location, float radius, List<ImmersalMapManager> immersalMapManager,List<ImmersalModelManager> immersalModelManager)
    {
        this.location = location;
        this.radius = radius;
        this.immersalMapManager=new List<ImmersalMapManager>(immersalMapManager);
        this.immersalModelManager=new List<ImmersalModelManager>(immersalModelManager);
    }
}
