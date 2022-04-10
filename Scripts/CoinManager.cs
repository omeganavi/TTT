using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public Text text;
    public int score;
    public void Start()
    {
        score = 0;
        if (instance == null)
        {
            instance = this;
        }
        text.text = CoinManager.instance.score.ToString();
    }

    public void Update()
    {
        text.text = CoinManager.instance.score.ToString();
    }

    public void ChangeScore(int coinValue)
    {
        CoinManager.instance.score += coinValue;

        text.text = CoinManager.instance.score.ToString();

    }
    public void buyweapon(int coinbuy)
    {
        CoinManager.instance.score = CoinManager.instance.score - coinbuy;
        text.text = CoinManager.instance.score.ToString();
    }
    public void buyhealth(int coinbuy)
    {
        CoinManager.instance.score = CoinManager.instance.score - coinbuy;
        PlayerController.instance.healthup(); 
    }
}