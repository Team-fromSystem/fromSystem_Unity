using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  OpenResult: MonoBehaviour
{
    [SerializeField] private ScreenCapture screenCapture;
    [SerializeField] private RawImage resultImage;
    [SerializeField] private Canvas result;
    [SerializeField] private PinchZoom pinchZoom;

    public void Open(){
        Debug.Log("OpenResult");
        resultImage.texture = screenCapture.ScreenShot;
        float rate = (float)resultImage.texture.width / resultImage.texture.height;
        float imageHeight = resultImage.rectTransform.sizeDelta.y;
        resultImage.rectTransform.sizeDelta = new Vector2(imageHeight * rate, imageHeight);
        resultImage.enabled=true;
    }

    public void Clese(){
        pinchZoom.ResetState();
        resultImage.enabled=false;
    }
}
