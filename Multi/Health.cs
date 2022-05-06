using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Health : MonoBehaviourPunCallbacks
{

    public float HealthAmount;
    public Image FillImage;

    public Player plMove;

    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public SpriteRenderer sr;
    public GameObject PlayerCanvas;



    private void Awake()
    {
        if (photonView.IsMine)
        {
            GameMaanager.Instance.LocalPlayer = this.gameObject;
        }
    }

    [PunRPC]
    public void ReduceHealth(float amount)
    {
        ModifyHealth(amount);
    }

    private void CheckHealth()
    {
        FillImage.fillAmount = HealthAmount / 100f;
        
        if(photonView.IsMine && HealthAmount <= 0)
        {
            GameMaanager.Instance.EnableRespawn();
            plMove.DisableInput = true;
            this.GetComponent<PhotonView>().RPC("Dead", RpcTarget.AllBuffered);
        }
        
    }

    public void EnableInput()
    {
        plMove.DisableInput = false;
    }

    [PunRPC]
    private void Dead()
    {
        rb.gravityScale = 0;
        bc.enabled = false;
        sr.enabled = false;
        PlayerCanvas.SetActive(false);
    }

    [PunRPC]
    private void Respawn()
    {
        rb.gravityScale = 0;
        bc.enabled = true;
        sr.enabled = true;
        PlayerCanvas.SetActive(true);
        FillImage.fillAmount = 1f;
        HealthAmount = 100f;
    }

    private void ModifyHealth(float amount)
    {
        if (photonView.IsMine)
        {
            HealthAmount -= amount;
            FillImage.fillAmount -= amount;
        }
        else
        {
            HealthAmount -= amount;
            FillImage.fillAmount -= amount;
        }
        
        CheckHealth();
        
    }

}
