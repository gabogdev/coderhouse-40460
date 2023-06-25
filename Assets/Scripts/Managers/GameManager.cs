using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Start,
    Play,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public static event Action MagnetEventEnd;
    public static event Action<GameState> GameStateEvent;
    
    [SerializeField] private int speedWorld = 5;
    [SerializeField] private int multiplierPointPerCoin = 10;

    public int BestScore => PlayerPrefs.GetInt(BEST_SCORE_KEY);
    public int Score => (int) distanceRunner + CoinsEarnedLevel * multiplierPointPerCoin;
    public float MultiplierValue { get; set; }
    public GameState CurrentState { get; set; }
    public int CoinsEarnedLevel { get; set; }

    private string BEST_SCORE_KEY = "MY_BEST_SCORE";
    private int bestScoreCheck;
    private float distanceRunner;

    private void Start()
    {
        MultiplierValue = 1f;
        bestScoreCheck = BestScore;
    }

    private void Update()
    {
        if(CurrentState == GameState.Start || CurrentState == GameState.GameOver)
        {
            return;
        }

        distanceRunner += Time.deltaTime * speedWorld * MultiplierValue;
    }

    public void ChangeState(GameState p_newState)
    {
        if(CurrentState != p_newState)
        {
            CurrentState = p_newState;
            GameStateEvent?.Invoke(CurrentState);
        }
    }

    public void InitScoreBooster(float time)
    {
        StartCoroutine(COScoreBooster(time));
    }

    private IEnumerator COScoreBooster(float time)
    {
        yield return new WaitForSeconds(time);
        MultiplierValue = 1;
    }

    private void UpdateBestScore()
    {
        if(Score > bestScoreCheck)
        {
            PlayerPrefs.SetInt(BEST_SCORE_KEY, Score);
        }
    }

    private void StateChangeEventResponse(GameState p_newState)
    {
        if(p_newState == GameState.GameOver)
        {
            UpdateBestScore();
        }
    }

    private IEnumerator COMagnetBooster(float time)
    {
        yield return new WaitForSeconds(time);
        MagnetEventEnd?.Invoke();
    }

    private void MagnetEventResponse(float durationTime)
    {
        StartCoroutine(COMagnetBooster(durationTime));
    }

    private void OnEnable()
    {
        GameStateEvent += StateChangeEventResponse;
        MagnetBooster.MagnetEvent += MagnetEventResponse;
    }

    private void OnDisable()
    {
        GameStateEvent -= StateChangeEventResponse;
        MagnetBooster.MagnetEvent -= MagnetEventResponse;
    }
}
