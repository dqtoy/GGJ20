using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelStatus
{
    Load, Play, CheckResult, Checkout
}

public class LevelManager : SingletonBehaviour<LevelManager>
{
    public LevelData currentLevel;
    public LevelStatus levelStatus;
    //public long levelStartTime;

    public Text Score;

    public void LoadLevel()
    {
        levelStatus = LevelStatus.Load;
        int previousLevelId = (currentLevel == null) ? -1 : currentLevel.id;
        currentLevel = LevelService.Instance.GetLevel(previousLevelId);

        CanvasManager.Instance.Init(currentLevel);
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
        int score = CanvasManager.Instance.grids.GetScore();
        Score.text = score.ToString();
    }

    public void EndLevel()
    {

    }
}
