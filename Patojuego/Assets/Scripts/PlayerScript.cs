using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScript : MonoBehaviour, IPunObservable
{
    [Header("Stats")]
    [SerializeField]
    private float speed = 600f;

    [SerializeField]
    private float jumpForce = 200f;

    private Rigidbody2D rb;
    private float desiredMovementAxis = 0f;

    private PhotonView pv;
    private Vector3 enemyPosition = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();

        //Callbacks por segundo
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 20;

    }

    private void Update()
    {
        if (pv.IsMine)
        {
            CheckInputs();
        }
        else
        {
            SmoothReplicate();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(desiredMovementAxis * Time.fixedDeltaTime * speed, rb.velocity.y);
    }

    private void CheckInputs()
    {
        desiredMovementAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Muerete();
        }
    }

    private void SmoothReplicate()
    {

        transform.position = Vector3.Lerp(transform.position, enemyPosition, Time.deltaTime * 20);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

    void IPunObservable.OnPhotonSerializeView(Photon.Pun.PhotonStream stream, Photon.Pun.PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            enemyPosition = (Vector3)stream.ReceiveNext();
        }
    }

    private void Muerete()
    {
        PhotonNetwork.Instantiate("Boolet", transform.position + new Vector3(1,0,1) , Quaternion.identity);
    }

    public void Damage()
    {
        pv.RPC("NetworkDamage", RpcTarget.All);
    }

    [PunRPC]
    public void NetworkDamage()
    {
        Application.Quit();
    }
}