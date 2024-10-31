using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionManager
{
    public float X;
    public float Y;
    public float Z;

    public PositionManager(){
        X=0.0f;
        Y=0.0f;
        Z=0.0f;
    }

    public PositionManager(float x,float y,float z){
        X=x;
        Y=y;
        Z=z;
    }
}
