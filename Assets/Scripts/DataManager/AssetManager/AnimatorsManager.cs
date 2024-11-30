using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimatorsManager
{
    public string name;
    public int animatorState;
    public int waitState;
    public int poseState;
    public int jumpState;
    public AnimatorsManager(string name,int animatorState,int waitState,int poseState,int jumpState){
        this.name=name;
        this.animatorState=animatorState;
        this.waitState=waitState;
        this.poseState=poseState;
        this.jumpState=jumpState;
    }
}
