using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ConsoleInput : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowStringInputField(Action<string> onSubmit)
    {
        gameObject.SetActive(true);

        GetComponent<TMP_InputField>().onSubmit.AddListener(delegate {
            onSubmit(GetComponent<TMP_InputField>().text);
            gameObject.SetActive(false);
        });
    }
}
