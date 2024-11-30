using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelListViewController : MonoBehaviour
{
    [SerializeField] private Canvas modelListView;
    [SerializeField] private bool isCreated;
    [SerializeField] private ChosedModelData chosedModelData;
    [SerializeField] private GameObject choseButton;
    public ModelManager thisData;
    public void OpenListView()
    {
        modelListView.enabled = true;
    }
    public void CloseListView()
    {
        if (isCreated)
        {
            chosedModelData.chosedData = thisData;
            choseButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chosedModelData.chosedData.modelName;
        }
        modelListView.enabled = false;
    }
    void Awake()
    {
        if (!isCreated)
        {
            return;
        }
        GameObject g = GameObject.FindWithTag("ModelListView");
        modelListView = g.GetComponent<Canvas>();
        this.GetComponent<Button>().onClick.AddListener(CloseListView);
        choseButton = GameObject.FindWithTag("ChoseButton");
    }
}
