using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    private static readonly Color BoundsColor = new Color(1, 0, 0, 0.5f);
    public SettingModelController settingModelController;
    public void Selected(){
        settingModelController.OpenCanvas(gameObject);
    }

    public void OnMouseDrag(){
        settingModelController.OpenCanvas(gameObject);
    }

    // void OnDrawGizmos()
    // {
    //     if (TryGetComponent(out Renderer renderer))
    //     {
    //         var bounds = renderer.bounds;

    //         Gizmos.color = BoundsColor;
    //         Gizmos.DrawCube(bounds.center, bounds.size);
    //     }
    // }
}
