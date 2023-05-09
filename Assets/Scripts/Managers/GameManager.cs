using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Play,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState CurrentState { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeState(GameState.Play);
        }
    }

    public void ChangeState(GameState p_newState)
    {
        if(CurrentState != p_newState)
        {
            CurrentState = p_newState;
        }
    }
}
