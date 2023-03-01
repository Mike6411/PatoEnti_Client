using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public enum ServerConnections { LOGINS, PING, REGISTER, RACEDATA, VERSION };

public class Network_Manager : MonoBehaviour
{
    public static Network_Manager _NETWORK_MANAGER;

    private TcpClient socket;
    private NetworkStream stream;

    private StreamWriter writer;
    private StreamReader reader;

    //Axio ho has de cambiar a la ip de la maquina i un altre port
    //port antic 10.40.2.185
    const string host = "192.168.1.43";
    const int port = 6543;

    private bool connected = false;

    private void Awake()
    {
        if (_NETWORK_MANAGER != null && _NETWORK_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _NETWORK_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (connected)
        {
            if (stream.DataAvailable)
            {
                string data  = reader.ReadLine();
                if(data != null)
                {
                    ManageData(data);
                }
            }
        }
    }

    private void ManageData(string data)
    {
        if(data == "Ping")
        {
            Debug.Log("Recibo Ping");
            writer.WriteLine("1");
            writer.Flush();
        }
    }

    public void ConnectToServer(ServerConnections conn, string[] parameters)
    {
        try
        {
            //Realizo connexion con el servidor
            socket = new TcpClient(host, port);
            connected = true;

            //Pilla el stream
            stream = socket.GetStream();

            //Almaceno  el canal de envio y de recepcion
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);

            switch (conn)
            {
                case ServerConnections.LOGINS:
                    writer.WriteLine("0" + "/" + parameters[0] + "/" + parameters[1]);
                    writer.Flush();
                    break;
                case ServerConnections.PING:
                    break;
                case ServerConnections.REGISTER:
                    writer.WriteLine("2" + "/" + parameters[0] + "/" + parameters[1] + "/" + parameters[2]);
                    writer.Flush();
                    break;
                case ServerConnections.RACEDATA:
                    break;
                case ServerConnections.VERSION:
                    break;
            }

        }
        catch { connected = false; }
    }
}
