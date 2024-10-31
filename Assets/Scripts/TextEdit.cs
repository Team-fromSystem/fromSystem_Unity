using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEdit : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        inputField = inputField.GetComponent<TMP_InputField>();
    }
    public void OnEnterInputField()
    {
        string inputValue = inputField.text; // InputFieldのテキスト値を取得
        Debug.Log("InputFieldの値: " + inputValue);
    }
}