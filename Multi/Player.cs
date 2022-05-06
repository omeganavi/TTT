using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;
    public Rigidbody2D rb;
    public Animator anime;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

    public bool IsGrounded = false;
    public float MoveSpeed;

    public GameObject BulletObject;
    public Transform FirePos;

    public bool DisableInput = false;


    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            PlayerNameText.text = photonView.Owner.NickName;
            PlayerNameText.color = Color.red;
        }
    }
    private void Update()
    {
        if (photonView.IsMine && !DisableInput)
        {
            CheckInput();
        }
    }
    private void CheckInput()
    {
        float movex = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        var move = new Vector3(movex, moveY).normalized;
        transform.position += (move * MoveSpeed * Time.fixedDeltaTime);


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletObject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChanegeDir_up", RpcTarget.AllBuffered);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletObject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChanegeDir_down", RpcTarget.AllBuffered);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletObject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChanegeDir_right", RpcTarget.AllBuffered);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletObject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChanegeDir_left", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            photonView.RPC("FlipTrue", RpcTarget.AllBuffered);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            photonView.RPC("FlipFalse", RpcTarget.AllBuffered);
        }
    }



    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }

    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }

}

