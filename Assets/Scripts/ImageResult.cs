using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageResult : MonoBehaviour
{
    [SerializeField] private ScreenCapture screenCapture;
    [SerializeField] private RawImage resultImage;
    [SerializeField] private GameObject resultImageObj;
    [SerializeField] private PinchZoom pinchZoom;

    public void OpenResult(){
        Debug.Log("OpenResult");
        resultImage.texture = screenCapture.ScreenShot;
        float rate = (float)resultImage.texture.width / resultImage.texture.height;
        float imageHeight = resultImage.rectTransform.sizeDelta.y;
        resultImage.rectTransform.sizeDelta = new Vector2(imageHeight * rate, imageHeight);
        resultImageObj.SetActive(true);
    }

    public void CleseResult(){
        pinchZoom.ResetState();
        resultImageObj.SetActive(false);
    }
}
