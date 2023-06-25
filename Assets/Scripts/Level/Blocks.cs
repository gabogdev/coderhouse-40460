using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    Normal,
    Wagons,
    Trains
}

public class Blocks : MonoBehaviour
{
    [SerializeField] private BlockType blockType;
    [SerializeField] private bool itHasRamp;

    [SerializeField] private GameObject[] coins;

    [SerializeField] private float minProbabilityBoost;
    [SerializeField] private GameObject[] boosters;

    public BlockType BlockTypes => blockType;
    public bool ItHasRamp => itHasRamp;

    private List<GameObject> coinsList = new List<GameObject>();
    private bool coinsReference;

    public void InitBlock()
    {
        GetCoins();
        ActivateCoins();
    }

    private void GetCoins()
    {
        if (!coinsReference)
        {
            foreach(GameObject parent in coins)
            {
                for(int j = 0; j < parent.transform.childCount; j++)
                {
                    GameObject coin = parent.transform.GetChild(j).gameObject;
                    coinsList.Add(coin);
                }
            }
        }

        coinsReference = true;
    }

    private void ActivateCoins()
    {
        if(coinsList.Count != 0 || coinsList != null)
        {
            foreach(GameObject coin in coinsList)
            {
                coin.SetActive(true);
            }
        }
    }

    private void BoosterSelect()
    {
        if(boosters != null || boosters.Length != 0)
        {
            for(int i = 0; i < boosters.Length; i++)
            {
                boosters[i].SetActive(false);
            }

            float randomProbability = Random.Range(0f, 100f);

            if(randomProbability <= minProbabilityBoost)
            {
                int itemRandomIndex = Random.Range(0, boosters.Length);
                boosters[itemRandomIndex].SetActive(true);
            }
        }
    }
}
