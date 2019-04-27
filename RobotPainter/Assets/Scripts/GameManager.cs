using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameStatus
{
    Title, Credits, Level
}

public class GameManager : SingletonBehaviour<GameManager>
{
    public GameStatus gameStatus = GameStatus.Level;

    public void Update()
    {
        if (gameStatus == GameStatus.Level)
            HandleInput_Level();

        if (gameStatus == GameStatus.Level)
            HandleInput_Level();
    }

    private void HandleInput_Title()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelManager.Instance.LoadLevel();
        }
    }

    private void HandleInput_Level()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            CanvasManager.Instance.ActionA();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            CanvasManager.Instance.ActionB();
        }
    }
}
