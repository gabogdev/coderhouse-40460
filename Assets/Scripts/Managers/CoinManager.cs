using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    public int TotalCoins { get; private set; }
    private string COIN_KEY = "MY_COINS";

    protected override void Awake()
    {
        base.Awake();
        TotalCoins = PlayerPrefs.GetInt(COIN_KEY);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddCoins(1);
        }
    }

    public void AddCoins(int p_coins)
    {
        TotalCoins += p_coins;
        PlayerPrefs.SetInt(COIN_KEY, TotalCoins);
        PlayerPrefs.Save();
    }

    public void SpendCoins(int p_coins)
    {
        if(TotalCoins >= p_coins)
        {
            TotalCoins -= p_coins;
            PlayerPrefs.SetInt(COIN_KEY, TotalCoins);
            PlayerPrefs.Save();
        }
    }
}
