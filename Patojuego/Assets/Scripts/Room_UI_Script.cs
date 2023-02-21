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

    private void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    public void CreateRoom()
    {
        Photon_Manager._PHOTON_MANAGER.CreateRoom(createInputField.text.ToString());
    }

    public void JoinRoom()
    {
        Photon_Manager._PHOTON_MANAGER.JoinRoom(joinInputField.text.ToString());
    }

}
