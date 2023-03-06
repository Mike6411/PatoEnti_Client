using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room_UI_Script : MonoBehaviour
{
    [SerializeField]
    public Button joinButton;

    [SerializeField]
    public Button createButton;

    [SerializeField]
    public InputField joinInputField;

    [SerializeField]
    public InputField createInputField;

    //Setup de los dos onClicks para la creacion de sala y unirse a sala
    private void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    //Llamada a la funcion de photon
    public void CreateRoom()
    {
        Photon_Manager._PHOTON_MANAGER.CreateRoom(createInputField.text.ToString());
    }

    //Llamada a la funcion de photon
    public void JoinRoom()
    {
        Photon_Manager._PHOTON_MANAGER.JoinRoom(joinInputField.text.ToString());
    }

}
