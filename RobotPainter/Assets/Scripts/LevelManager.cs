using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelStatus
{
    Load, Play, CheckResult, Checkout, Fail
}

public class LevelManager : SingletonBehaviour<LevelManager>
{
    public LevelData currentLevel;
    public LevelStatus levelStatus;
    public int level = 0;


    public Text levelText;

    public void LoadLevel()
    {
        levelStatus = LevelStatus.Load;
        int previousLevelId = (currentLevel == null) ? -1 : currentLevel.id;
        currentLevel = LevelService.Instance.GetLevel(previousLevelId);

        level++;
        levelText.text = level.ToString();
        bool withHelp = (level < 5);
        CanvasManager.Instance.Init(currentLevel, withHelp);
        StartLevel();
    }

    public void StartLevel()
    {
        levelStatus = LevelStatus.Play;
        CanvasManager.Instance.StartPlay();
    }

    public void CheckResult()
    {
        levelStatus = LevelStatus.CheckResult;
        CanvasManager.Instance.ShowChecking();
        int till = RhythmManager.Instance.GetTillNextOddBeatTime() / 1000;
        Invoke("ShowScore", till);
    }

    public void ShowScore()
    {
        int score = CanvasManager.Instance.grids.GetScore();
        int totalScore = CanvasManager.Instance.grids.GetTotalScore();
        float s = (float)score / totalScore;
        if (s > 0.6f)
        {
            CanvasManager.Instance.ShowOK();
            CheckOut(score);
        }
        else
        {
            CanvasManager.Instance.ShowNO();
            EndLevel();
        }
    }

    public void CheckOut(int score)
    {
        levelStatus = LevelStatus.Checkout;
        HealthManager.Instance.AddHealth(score);
        LevelManager.Instance.LoadLevel();
    }

    public void EndLevel()
    {
        levelStatus = LevelStatus.Fail;
    }

    public void Restart()
    {
        level = 0;
        levelText.text = level.ToString();
        LoadLevel();
    }
}
