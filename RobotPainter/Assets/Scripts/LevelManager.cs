﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelStatus
{
    Load, Play, CheckResult, Checkout
}

public class LevelManager : SingletonBehaviour<LevelManager>
{
    public LevelData currentLevel;
    public LevelStatus levelStatus;
    public long levelStartTime;

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
        levelStartTime = DateTimeUtil.GetUnixTimeMilliseconds();
    }

    public void CheckResult()
    {
        levelStatus = LevelStatus.CheckResult;

    }

    public void EndLevel()
    {

    }
}
