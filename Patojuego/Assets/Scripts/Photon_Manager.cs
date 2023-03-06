using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Photon_Manager : MonoBehaviourPunCallbacks
{
    public static Photon_Manager _PHOTON_MANAGER;

    //string vacia para cuando el COnnect TO server no necesita una string
    string[] vacio = new string[1];

    private void Awake()
    {
        if ( _PHOTON_MANAGER != null && _PHOTON_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _PHOTON_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);

            //Realizo la connexion
            PhotonConnect();

            Network_Manager._NETWORK_MANAGER.ConnectToServer(ServerConnections.DBCHECK , vacio);
            Network_Manager._NETWORK_MANAGER.ConnectToServer(ServerConnections.RACEDATA, vacio);

            Scene_Manager.sceneManager.LoadLoginRegisterScene();
        }
    }

    public void PhotonConnect()
    {
        //Syncroniza la carga de los jugadores
        PhotonNetwork.AutomaticallySyncScene = true;

        //Connexion al server con los settings establecidos (PhotonUnityNetworking => Resources)
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Se ha realizado la connexion");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("He implosionado porque" + cause);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("He entrado al lobby");
    }

    public void CreateRoom(string NameRoom)
    {
        PhotonNetwork.CreateRoom(NameRoom, new RoomOptions { MaxPlayers = 2 });
        Debug.Log("Sala Creada");
    }

    public void JoinRoom(string NameRoom)
    {
        PhotonNetwork.JoinRoom(NameRoom);
        Debug.Log("Hola");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Me he unido a la sala" + PhotonNetwork.CurrentRoom.Name + " con " + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores contectados a ella");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No me he conectado por" + returnCode + "que significa" + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount ==  PhotonNetwork.CurrentRoom.MaxPlayers && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Ingame");
        }
    }

}
