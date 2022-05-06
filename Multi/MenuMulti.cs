using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class MenuMulti : MonoBehaviourPunCallbacks
{
    
    [SerializeField] public string VersionName = "2.17";
    [SerializeField] public GameObject UsernameMenu;
    [SerializeField] public GameObject ConnectPanel;

    [SerializeField] public InputField UsernameInput;
    [SerializeField] public InputField CreateGameInput;
    [SerializeField] public InputField JoinGameInput;

    [SerializeField] public GameObject StartButton;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    void Start()
    {
        UsernameMenu.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChangeuserNameInput() { 
        if(UsernameInput.text.Length >= 3){
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        } 
    }
    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.NickName = UsernameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }
    public void JoinGame(){
        RoomOptions roomoptions = new RoomOptions();
        roomoptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomoptions, TypedLobby.Default);

    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MultiGame");
    }
    
 

}
