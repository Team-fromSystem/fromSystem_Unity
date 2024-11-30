using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class SettingModelController : MonoBehaviour
{
    [SerializeField] private Canvas thisCanvas;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas listCanvas;
    [SerializeField] private Sprite sprite_X;
    [SerializeField] private Sprite sprite_Y;
    [SerializeField] private Sprite sprite_Z;
    [SerializeField] private Slider rotetoSlider;
    [SerializeField] private Image buttonImg;
    [SerializeField] private TMP_InputField rotetoInputField;
    [SerializeField] private Slider sizeSlider;
    [SerializeField] private TMP_InputField sizeInputField;
    [SerializeField] private int targetAxis = 1;//1>Y,2>X,3>Z
    public GameObject targetGO;
    [SerializeField] private Transform goTransform;
    [SerializeField] private CreateModelController createModelController;
    [SerializeField] private AnimatorsData animatorsData;
    [SerializeField] private TMP_Dropdown animatorDropdown;
    private int animatorV;
    private int waitV;
    private int poseV;
    private int jumpV;

    public void Awake()
    {
        animatorDropdown.ClearOptions();
        var data = animatorsData.animatorsManager.Select(el => el.name);
        List<string> nameData = new List<string>(data);
        animatorDropdown.AddOptions(nameData);
        animatorDropdown.value = 0;
    }

    public void ChangeTargetAxis()
    {
        var rote = goTransform.eulerAngles;
        switch (targetAxis % 3)
        {
            case 0:
                targetAxis = 1;
                SetRotateValue(rote.y);
                buttonImg.sprite = sprite_Y;
                break;
            case 1:
                targetAxis = 2;
                SetRotateValue(rote.x);
                buttonImg.sprite = sprite_X;
                break;
            case 2:
                targetAxis = 3;
                SetRotateValue(rote.z);
                buttonImg.sprite = sprite_Z;
                break;
        }
    }

    void SetRotateValue(float value)
    {
        rotetoSlider.value = value;
        rotetoInputField.text = value.ToString();
    }
    public void ChangeRotation(float value)
    {
        var rote = goTransform.eulerAngles;
        switch (targetAxis)
        {
            case 1:
                rote.y = value;
                break;
            case 2:
                rote.x = value;
                break;
            case 3:
                rote.z = value;
                break;
        }
        goTransform.eulerAngles = rote;
    }
    public void ChangeRotationAsText()
    {
        var value = float.Parse(rotetoInputField.text);
        if (value > 360)
        {
            value = value % 360;
            rotetoInputField.text = value.ToString();
        }
        rotetoSlider.value = value;
        ChangeRotation(value);
    }
    public void ChangeRotationAsSlider()
    {
        var value = rotetoSlider.value.ToString();
        rotetoInputField.text = value;
        ChangeRotation(rotetoSlider.value);
    }
    public void SetSizeValue(float value)
    {
        sizeSlider.value = value;
        sizeInputField.text = value.ToString();
    }
    public void ChangeSize(float value)
    {
        goTransform.localScale = Vector3.one * value;
    }
    public void ChangeSizeAsText()
    {
        var value = float.Parse(sizeInputField.text);
        if (value > 100)
        {
            value = 100;
            sizeInputField.text = value.ToString();
        }
        sizeSlider.value = value;
        ChangeSize(value);
    }
    public void ChangeSizeAsSlider()
    {
        var value = sizeSlider.value.ToString();
        sizeInputField.text = value;
        ChangeSize(sizeSlider.value);
    }
    public void ChangeAnimator()
    {
        int index = animatorDropdown.value;
        Debug.Log($"index={index}");
        var data = animatorsData.animatorsManager[index];
        if (targetGO.TryGetComponent<Animator>(out Animator animator))
        {
            Debug.Log("GetAnimator");
            animator.SetInteger("ChangeAnimator", data.animatorState);
            animator.SetInteger("ChangeWait", data.waitState);
            animator.SetInteger("ChangePose", data.poseState);
            animator.SetInteger("ChangeJump", data.jumpState);
        }
    }
    public void SetDDValue(Animator animator)
    {
        Debug.Log("GetAnimator");
        animatorV = animator.GetInteger("ChangeAnimator");
        switch (animatorV)
        {
            case 0:
                break;
            case 1:
                waitV = animator.GetInteger("ChangeWait");
                break;
            case 2:
                poseV = animator.GetInteger("ChangePose");
                break;
            case 3:
                jumpV = animator.GetInteger("ChangeJump");
                break;
        }
        var index = animatorsData.animatorsManager.FindIndex(x => x.animatorState == animatorV && x.waitState == waitV && x.poseState == poseV && x.jumpState == jumpV);
        animatorDropdown.value = index;
    }
    public void DestroyGO()
    {
        createModelController.objectList.Remove(targetGO);
        Destroy(targetGO);
        CloseCanvas();
    }
    public void OpenCanvas(GameObject go)
    {
        if (EventSystem.current.currentSelectedGameObject != null||listCanvas.enabled==true)
        {
            return;
        }
        mainCanvas.enabled = false;
        thisCanvas.enabled = true;
        targetGO = go;
        goTransform = targetGO.transform;
        var rote = goTransform.eulerAngles;
        var size = goTransform.localScale;
        SetRotateValue(rote.y);
        SetSizeValue(size.y);
        if (targetGO.TryGetComponent<Animator>(out Animator animator))
        {
            if (animator.isHuman)
            {
                animatorDropdown.gameObject.SetActive(true);
                SetDDValue(animator);
                return;
            }
        }
        animatorDropdown.gameObject.SetActive(false);
    }
    public void CloseCanvas()
    {
        thisCanvas.enabled = false;
        targetAxis = 1;
        buttonImg.sprite = sprite_Y;
        targetGO = null;
        goTransform = null;
        animatorV = 0;
        waitV = 0;
        poseV = 0;
        jumpV = 0;
        mainCanvas.enabled = true;
    }
}
