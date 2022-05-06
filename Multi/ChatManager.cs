using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviour, Photon.Pun.IPunObservable
{
    private Player plMove;
    public PhotonView photonView;
    public GameObject BuubleSpeechObject;
    public Text UpdateText;
    private InputField ChatInputfield;
    private bool DisableSend;


    private void Awake()
    {
        ChatInputfield = GameObject.Find("ChatInputField").GetComponent<InputField>();
    }

    private void Update()
    {
        if (photonView.IsMine){
        
            if(!DisableSend && ChatInputfield.isFocused)
            {
                if(ChatInputfield.text !="" && ChatInputfield.text.Length > 0 && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    photonView.RPC("SendMessage", RpcTarget.AllBuffered, ChatInputfield.text);
                    BuubleSpeechObject.SetActive(true);
                    ChatInputfield.text = "";
                    DisableSend = true;
 
                }
            }
        }
    }

    [PunRPC]
    private void SendMessage(string message)
    {
        UpdateText.text = message;
        StartCoroutine("Remove");
    }

    IEnumerable Remove()
    {
        yield return new WaitForSeconds(4f);
        BuubleSpeechObject.SetActive(false);
        DisableSend = false;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting)
        {
            stream.SendNext(BuubleSpeechObject.active);
        } else if (stream.IsReading){
            BuubleSpeechObject.SetActive((bool)stream.ReceiveNext());
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
