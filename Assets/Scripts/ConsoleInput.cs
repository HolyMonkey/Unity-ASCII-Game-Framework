using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ConsoleInput : MonoBehaviour
{
    public GameObject InputField;
    public delegate void GetFunc (string input);

    private GameObject _inputField;

    public void ShowstringInputField(GetFunc OnEnter)
    {
        _inputField = Instantiate(InputField);
        _inputField.GetComponentInChildren<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

        _inputField.GetComponentInChildren<TMP_InputField>().onEndEdit.AddListener(delegate { OnEnter(_inputField.GetComponentInChildren<TMP_InputField>().text); Destroy(_inputField); });
    }
}
