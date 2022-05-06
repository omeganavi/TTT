using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class GameMaanager : MonoBehaviour
{
    public static GameMaanager Instance;

    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject ScreenCamera;
    public Text PingText;
    public GameObject disconnectUI;
    private bool Off = false;

    public GameObject PlayerFeed;
    public GameObject FeedGrid;

    [HideInInspector] public GameObject LocalPlayer;
    public Text RespawnTimerText;
    public GameObject RespawnMenu;
    private float TimerAmount = 5f;
    private bool RunSpawnTimer = false;

    public void Awake()
    {
        Instance = this;
        GameCanvas.SetActive(true);
    }

    

    public void CheckInput()
    {
        if(Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            Off = false;
        } else if (!Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(true);
            Off = true;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }



    private void Update()
    {
        CheckInput();
        PingText.text = "Ping:" + PhotonNetwork.GetPing();

        if (RunSpawnTimer)
        {
            StartRespawn();
        }
    }

    private void StartRespawn()
    {
        TimerAmount -= Time.deltaTime;
        RespawnTimerText.text = "Apareciendo en " + Math.Round(TimerAmount, 0).ToString();

        if (TimerAmount <= 0)
        {
            LocalPlayer.GetComponent<PhotonView>().RPC("Respawn", RpcTarget.AllBuffered);
            LocalPlayer.GetComponent<Health>().EnableInput();
            RespawnLocation();
            RespawnMenu.SetActive(false);
            RunSpawnTimer = false;
        }
    }


    public void RespawnLocation()
    {
        float randomValue = UnityEngine.Random.Range(1, 4);
        if (randomValue == 1)
        {
            LocalPlayer.transform.localPosition = new Vector2(25f, -23f);
        }
        if (randomValue == 2)
        {
            LocalPlayer.transform.localPosition = new Vector2(100f, -57f);
        }
        if (randomValue == 3)
        {
            LocalPlayer.transform.localPosition = new Vector2(71f, -100f);
        }
        if (randomValue == 4)
        {
            LocalPlayer.transform.localPosition = new Vector2(170f, -100f);
        }
    }


    public void EnableRespawn()
    {
        TimerAmount = 5f;
        RunSpawnTimer = true;
        RespawnMenu.SetActive(true);
    }

    public void SpawnPlayer()
    {
        float randomValue = UnityEngine.Random.Range(1, 4);
        if (randomValue == 1)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * -20, this.transform.position.y * -5), Quaternion.identity, 0);
            GameCanvas.SetActive(false);
            ScreenCamera.SetActive(false);
        }
        if (randomValue == 2)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * -80, this.transform.position.y * -20), Quaternion.identity, 0);
            GameCanvas.SetActive(false);
            ScreenCamera.SetActive(false);
        }
        if (randomValue == 3)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * -60, this.transform.position.y * -30), Quaternion.identity, 0);
            GameCanvas.SetActive(false);
            ScreenCamera.SetActive(false);
        }
        if (randomValue == 4)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * -100, this.transform.position.y * -30), Quaternion.identity, 0);
            GameCanvas.SetActive(false);
            ScreenCamera.SetActive(false);
        }

    }

    private void OnPhotonPlayerConnected(Photon.Realtime.Player player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.NickName + "joined the game";
        obj.GetComponent<Text>().color = Color.green;
    }

    private void OnPhotonPlayerDisconnected(Photon.Realtime.Player player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.NickName + "left the game";
        obj.GetComponent<Text>().color = Color.red;
    }

}
