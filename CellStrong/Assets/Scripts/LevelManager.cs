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
    public int levelId;
    public LevelStatus status;

    public void Update()
    {
        if (status != LevelStatus.Playing)
            return;
    }

    public void StartLevel()
    {
        LoadLevel();

        status = LevelStatus.Playing;
        PlayerManager.Instance.SpawnPlayer();
        PieceManager.Instance.StartGenerating();
    }

    private void LoadLevel()
    {

    }

    public bool CanMovePlayer()
    {
        return true;
    }

    public bool CanRotatePlayer()
    {
        return true;
    }

    public bool CanReleasePiece()
    {
        return true;
    }
}
