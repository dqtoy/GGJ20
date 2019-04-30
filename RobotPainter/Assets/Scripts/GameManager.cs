using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Title, Credits, Level
}

public class GameManager : SingletonBehaviour<GameManager>
{
    public Transform StartText;
    public Transform RestartText;
    public Transform Tutorial;
    public GameStatus gameStatus = GameStatus.Title;
    public bool debug = false;

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
            StartText.gameObject.SetActive(false);
            Tutorial.gameObject.SetActive(true);
            LevelManager.Instance.LoadLevel();
            gameStatus = GameStatus.Level;
            MusicManager.Instance.PlayBGMusic();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //CanvasManager.Instance.ShowOK();
        }
    }

    private void HandleInput_Level()
    {
        /*
        if (LevelManager.Instance.levelStatus == LevelStatus.CheckResult)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LevelManager.Instance.LoadLevel();
            }
            return;
        }
        */

        if (LevelManager.Instance.levelStatus == LevelStatus.Fail)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartText.gameObject.SetActive(false);
                LevelManager.Instance.Restart();
            }
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
