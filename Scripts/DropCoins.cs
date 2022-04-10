using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DropCoins : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoinManager.instance.ChangeScore(coinValue);
        }

    }
}
