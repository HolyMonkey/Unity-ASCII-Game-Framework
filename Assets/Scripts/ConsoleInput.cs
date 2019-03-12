using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ConsoleInput : MonoBehaviour
{
    public GameObject InputField;
    public Camera MainCamera;

    public void ShowstringInputField(Action<string> onSubmit)
    {
        InputField.SetActive(true);
        InputField.GetComponentInChildren<Canvas>().worldCamera = MainCamera;

        InputField.GetComponentInChildren<TMP_InputField>().onSubmit.AddListener(delegate {
            onSubmit(InputField.GetComponentInChildren<TMP_InputField>().text);
            InputField.SetActive(false);
        });
    }
}
