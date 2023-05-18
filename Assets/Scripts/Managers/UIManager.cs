using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private GameObject GameOverMenuPanel;
    
    [SerializeField] private TextMeshProUGUI accumulatedCoinsTMP;
    [SerializeField] private TextMeshProUGUI totalCoinsTMP;
    [SerializeField] private TextMeshProUGUI gameOverCoinsTMP;
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI gameOverScoreTMP;
    [SerializeField] private TextMeshProUGUI bestScoreTMP;

    private void Start()
    {
        UpdateMenu();
    }

    private void Update()
    {
        accumulatedCoinsTMP.text = GameManager.Instance.CoinsEarnedLevel.ToString();
        scoreTMP.text = GameManager.Instance.Score.ToString();
    }

    private void UpdateMenu()
    {
        totalCoinsTMP.text = CoinManager.Instance.TotalCoins.ToString();
        bestScoreTMP.text = GameManager.Instance.BestScore.ToString();
    }

    private void UpdateGameOver()
    {
        GameOverMenuPanel.SetActive(true);
        gameOverScoreTMP.text = GameManager.Instance.Score.ToString();
        gameOverCoinsTMP.text = GameManager.Instance.CoinsEarnedLevel.ToString();
    }

    public void Play()
    {
        startMenuPanel.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Play);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResponseChangeState(GameState p_newState)
    {
        if(p_newState == GameState.GameOver)
        {
            UpdateGameOver();
        }
    }

    private void OnEnable()
    {
        GameManager.GameStateEvent += ResponseChangeState;
    }

    private void OnDisable()
    {
        GameManager.GameStateEvent -= ResponseChangeState;
    }
}
