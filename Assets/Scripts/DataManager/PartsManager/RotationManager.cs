using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RotationManager
{
    public float X;
    public float Y;
    public float Z;

    public RotationManager(){
        X=0.0f;
        Y=0.0f;
        Z=0.0f;
    }

    public RotationManager(float x,float y,float z){
        X=x;
        Y=y;
        Z=z;
    }
}
