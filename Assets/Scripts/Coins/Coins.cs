using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float colliderNewSize = 5f;
    [SerializeField] private float minDistancePlayer = 1.5f;
    [SerializeField] private float moveSpeed = 12f;

    public Transform Player { get; set; }

    private new BoxCollider collider;
    private Vector3 initSize;
    private bool activateMagnet;


    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        initSize = collider.size;
    }

    private void Update()
    {
        if(Player != null)
        {
            Debug.DrawLine(transform.position, Player.position, Color.blue);
            MoveCoin();
        }
    }

    private void GetCoin()
    {
        SoundManager.Instance.PlaySoundFX(SoundManager.Instance.itemClip);
        CoinManager.Instance.AddCoins(coinValue);
        GameManager.Instance.CoinsEarnedLevel += coinValue;
        gameObject.SetActive(false);
    }

    private void MoveCoin()
    {
        if(Vector3.Distance(Player.position, transform.position) > 0.1f)
        {
            if(Vector3.Distance(Player.position, transform.position) < minDistancePlayer)
            {
                GetCoin();
            }

            transform.position = Vector3.MoveTowards(transform.position, Player.position + Vector3.up, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider coin)
    {
        if (coin.CompareTag("Player"))
        {
            if (activateMagnet)
            {
                Player = coin.transform;
            }
            else
            {
                GetCoin();
            }
        }
    }

    private void MagnetEventResponse(float durationTime)
    {
        collider.size *= colliderNewSize;
        activateMagnet = true;
    }

    private void MagnetEventResponseEnd()
    {
        collider.size = initSize;
        activateMagnet = false;
    }

    private void OnEnable()
    {
        MagnetBooster.MagnetEvent += MagnetEventResponse;
        GameManager.MagnetEventEnd += MagnetEventResponseEnd;
    }

    private void OnDisable()
    {
        MagnetBooster.MagnetEvent -= MagnetEventResponse;
        GameManager.MagnetEventEnd -= MagnetEventResponseEnd;
    }
}
