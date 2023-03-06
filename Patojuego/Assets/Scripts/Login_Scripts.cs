using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login_Scripts : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private InputField loginText;
    [SerializeField] private InputField passwordText;

    //Setup del listener para que cuando lo cliques se ejecute Func
    private void Awake()
    {
        loginButton.onClick.AddListener(Func);
    }

    //Coje los valores de la UI de login y los pasa al login como valores mediante la funcion ConnectToServer
    private void Func()
    {
        string[] data = new string[2];
        data[0] = loginText.text.ToString();
        data[1] = passwordText.text.ToString();

        Debug.Log(data[0]);

        Network_Manager._NETWORK_MANAGER.ConnectToServer(ServerConnections.LOGINS , data);

    }
}
