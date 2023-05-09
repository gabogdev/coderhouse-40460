using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private void GetCoin()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider coin)
    {
        if (coin.CompareTag("Player"))
        {
            GetCoin();
        }
    }

}
