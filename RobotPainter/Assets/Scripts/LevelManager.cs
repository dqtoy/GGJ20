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

    private int score;
    private int totalScore;

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
        if (levelStatus == LevelStatus.CheckResult)
            return;

        levelStatus = LevelStatus.CheckResult;
        CanvasManager.Instance.ShowChecking();
        int till = RhythmManager.Instance.GetTillNextQuaterBeatTime() / 1000;
        Invoke("ShowScore", till);
    }

    public void ShowScore()
    {
        score = CanvasManager.Instance.grids.GetScore();
        totalScore = CanvasManager.Instance.grids.GetTotalScore();
        float s = (float)score / totalScore;
        if (s > 0.6f)
        {
            CanvasManager.Instance.ShowOK();

            int till = RhythmManager.Instance.GetTillNextQuaterBeatTime() / 1000;
            Invoke("CheckOut", till);
        }
        else
        {
            CanvasManager.Instance.ShowNO();

            int till = RhythmManager.Instance.GetTillNextDoubleQuaterBeatTime() / 1000;
            Invoke("EndLevel", till);
        }
    }

    public void CheckOut()
    {
        levelStatus = LevelStatus.Checkout;
        HealthManager.Instance.AddHealth(score);
        int till = RhythmManager.Instance.GetTillNextDoubleQuaterBeatTime() / 1000;
        Invoke("LoadLevel", till);
    }

    public void EndLevel()
    {
        levelStatus = LevelStatus.Fail;
        CanvasManager.Instance.ShowEmpty();
        GameManager.Instance.RestartText.gameObject.SetActive(true);
    }

    public void Restart()
    {
        level = 0;
        levelText.text = level.ToString();
        LoadLevel();
    }
}
