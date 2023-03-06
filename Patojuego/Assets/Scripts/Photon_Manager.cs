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

    //Connect inicial a Photon
    public void PhotonConnect()
    {
        //Syncroniza la carga de los jugadores
        PhotonNetwork.AutomaticallySyncScene = true;

        //Connexion al server con los settings establecidos (PhotonUnityNetworking => Resources)
        PhotonNetwork.ConnectUsingSettings();
    }

    //Control de connexion al host
    public override void OnConnectedToMaster()
    {
        Debug.Log("Se ha realizado la connexion");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    //Control de la desconexion de jugadores que directamente tanca la partida
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("He implosionado porque" + cause);
        Application.Quit();
    }

    //Aviso de entrada exitosa al lobby del server de Photon
    public override void OnJoinedLobby()
    {
        Debug.Log("He entrado al lobby");
    }

    //Aviso de creacion exitosa de la sala
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

    //Aviso de que se ha entrado en la sala indiferentemente de si la has creado o has entrado despues de que haya sido creada
    public override void OnJoinedRoom()
    {
        Debug.Log("Me he unido a la sala" + PhotonNetwork.CurrentRoom.Name + " con " + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores contectados a ella");
    }

    //Aviso de que no has entrado
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No me he conectado por" + returnCode + "que significa" + message);
    }

    //Aviso de que un jugador remoto ha entrado en la sala y ya podemos jugar
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount ==  PhotonNetwork.CurrentRoom.MaxPlayers && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Ingame");
        }
    }

}
