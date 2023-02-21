using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register_Script : MonoBehaviour
{
    [SerializeField] private Button registerButton;
    [SerializeField] private InputField registerText;
    [SerializeField] private InputField passwordText;
    [SerializeField] private InputField raceText;

    private void Awake()
    {
        registerButton.onClick.AddListener(Func);
    }

    private void Func()
    {
        string[] data = new string[3];
        data[0] = registerText.text.ToString();
        data[1] = passwordText.text.ToString();
        data[2] = raceText.text.ToString();

        Debug.Log(data[0]);

        Network_Manager._NETWORK_MANAGER.ConnectToServer(ServerConnections.REGISTER , data);

    }
}
