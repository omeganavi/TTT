using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BulletMulti : MonoBehaviourPunCallbacks, IPunObservable
{
    public int MoveDir = 0;
    public float MoveSpeed;
    public float DestroyTime;
    public float BulletDamage;

    private void Awake()
    {
        StartCoroutine("DestroyByTime");
    }

    public IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(DestroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ChanegeDir_left()
    {
        MoveDir = 3;
    }

    [PunRPC]
    public void ChanegeDir_up()
    {
        MoveDir = 1;
    }

    [PunRPC]
    public void ChanegeDir_down()
    {
        MoveDir = 2;
    }

    [PunRPC]
    public void ChanegeDir_right()
    {
        MoveDir = 4;
    }

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {

        
        if (MoveDir == 1)
        {
            transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        }
        if (MoveDir == 2)
        {
            transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
        }
        if (MoveDir == 3)
        {
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }
        if (MoveDir == 4)
        {
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (target != null && (!target.IsMine || target.IsSceneView))
        {
            target.RPC("ReduceHealth", RpcTarget.AllBuffered, BulletDamage);
            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

}

