using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PinchZoom : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 mouseStartPos;
    float dist0 = 0f;
    float dist1 = 0f;
    float scale = 0f;
    float oldDist = 0f;//前回の2点間の距離
    float minRate = 0.7f;
    float maxRate = 3f;
    Vector3 v = Vector3.zero;
    Vector2 firstPs;

    public void ResetState()
    {
        oldDist = 0f;
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.position = firstPs;
    }
    void Awake(){
        firstPs=transform.position;
    }
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch t1 = Input.GetTouch(0);
            if (t1.phase == TouchPhase.Began)
            {
                //移動したいUIオブジェクトの最初の位置
                startPos = transform.position;
                //ドラッグを開始したときのマウスの位置
                mouseStartPos = t1.position;
            }
            else if (t1.phase == TouchPhase.Moved)
            {
                //ドラッグ中のUIオブジェクトの位置
                transform.position
                        = startPos + (t1.position - mouseStartPos);
            }
        }
        if (Input.touchCount >= 2)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);
            if (t2.phase == TouchPhase.Began)
            {
                dist0 = Vector2.Distance(t1.position, t2.position);
                oldDist = dist0;
            }
            else if (t1.phase == TouchPhase.Moved && t2.phase == TouchPhase.Moved)
            {
                dist1 = Vector2.Distance(t1.position, t2.position);
                if (dist0 < 0.001f || dist1 < 0.001f)
                {
                    return;
                }
                else
                {
                    v = transform.localScale;
                    scale = v.x;
                    scale += (dist1 - oldDist) / 200f;
                    if (scale > maxRate) { scale = maxRate; }
                    if (scale < minRate) { scale = minRate; }
                    oldDist = dist1;
                }
                transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}