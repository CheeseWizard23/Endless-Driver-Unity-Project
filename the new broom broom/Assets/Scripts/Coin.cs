using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("CoinPickup");
            PlayerManager.numOfCoins += 1;
            PlayerManager.totalNumOfCoins += 1;
            PlayerManager.score += 2;
            PlayerPrefs.SetInt("TotalNumberOfCoins", PlayerManager.totalNumOfCoins);
            Destroy(gameObject);
        }
    }
}
