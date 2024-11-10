using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    Slider ySlider;
    [SerializeField] private PlaneCreateModel planeCreateModel;
 
    // Use this for initialization
    void Start()
    {
 
        ySlider = GetComponent<Slider>();
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
 
    public void Method()
    {
        if(planeCreateModel.instantObject!=null){
            planeCreateModel.instantObject.transform.rotation=Quaternion.Euler( 0f, ySlider.value, 0f);
        }
    }
}
