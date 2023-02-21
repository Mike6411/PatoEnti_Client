using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Muere : MonoBehaviourPun
{
    [SerializeField]
    private float speed = 10f;
    private Rigidbody2D rb;


    private PhotonView pv;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerScript>().Damage();
        pv.RPC("NetworkDestroy", RpcTarget.All);
    }

    [PunRPC]
    public void NetworkDestroy()
    {
        Destroy(this.gameObject);

    }
}
