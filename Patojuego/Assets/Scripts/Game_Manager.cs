using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnplayer1;

    [SerializeField]
    private GameObject spawnplayer2;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", spawnplayer1.transform.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Player", spawnplayer2.transform.position, Quaternion.identity);
        }
    }
}
