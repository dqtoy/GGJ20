using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelStatus
{
    Idle,
    Playing,
    Win,
    Lose
}

public class LevelManager : SingletonBehaviour<LevelManager>
{
    public LevelStatus status;

    public void Update()
    {
        if (status != LevelStatus.Playing)
            return;
    }

    public void StartLevel()
    {
        status = LevelStatus.Playing;
    }
}
