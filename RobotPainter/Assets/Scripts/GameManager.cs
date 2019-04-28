using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameStatus
{
    Title, Credits, Level
}

public class GameManager : SingletonBehaviour<GameManager>
{
    public GameStatus gameStatus = GameStatus.Title;

    public void Update()
    {
        if (gameStatus == GameStatus.Title)
            HandleInput_Title();

        if (gameStatus == GameStatus.Level)
            HandleInput_Level();
    }

    private void HandleInput_Title()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelManager.Instance.LoadLevel();
            gameStatus = GameStatus.Level;
        }
    }

    private void HandleInput_Level()
    {
        if (LevelManager.Instance.levelStatus == LevelStatus.CheckResult)
        {
            LevelManager.Instance.LoadLevel();
            return;
        }

        if (LevelManager.Instance.levelStatus == LevelStatus.Play)
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
}
