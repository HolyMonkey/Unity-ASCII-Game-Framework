using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ConsoleInput : MonoBehaviour
{
    public TMP_InputField InputField;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowStringInputField(Action<string> onSubmit)
    {
        gameObject.SetActive(true);

        InputField.onSubmit.AddListener(delegate {
            onSubmit(InputField.text);
            gameObject.SetActive(false);
        });
    }
}
