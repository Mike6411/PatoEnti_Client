using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login_Scripts : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private InputField loginText;
    [SerializeField] private InputField passwordText;

    private void Awake()
    {
        loginButton.onClick.AddListener(Func);
    }

    private void Func()
    {
        string[] data = new string[2];
        data[0] = loginText.text.ToString();
        data[1] = passwordText.text.ToString();

        Debug.Log(data[0]);

        Network_Manager._NETWORK_MANAGER.ConnectToServer(ServerConnections.LOGINS , data);

    }
}
