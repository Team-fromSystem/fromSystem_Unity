using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Decoration : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();

    public TMP_Dropdown items_Dropdown;

    private GameObject itemObject;

    public GameObject ItemObject{
        get{
            return itemObject;
        }
        set{
            itemObject=value;
        }
    }

    public PlaneCreateModel planeCreateModel;


    public void ChangeItem()
    {
        itemObject = itemList[items_Dropdown.value];
    }
    public void AddContent()
    {
        if(itemObject==null){
            return;
        }
        Transform cameraTransform = Camera.main.transform;
        GameObject go = Instantiate(itemObject, cameraTransform.position + cameraTransform.forward, Quaternion.identity, planeCreateModel.instantObject.transform);
        planeCreateModel.itemObjectList.Add(go);
    }
}
