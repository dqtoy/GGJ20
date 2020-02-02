using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (LevelManager.Instance.status == LevelStatus.Idle)
            PreGameInput();
        if (LevelManager.Instance.status == LevelStatus.Playing)
            InGameInput();
        if (LevelManager.Instance.status == LevelStatus.Win
            || LevelManager.Instance.status == LevelStatus.Lose)
            PostGameInput();
    }

    private void PreGameInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelManager.Instance.StartLevel();
        }
    }

    private void PostGameInput()
    {

    }

    private void InGameInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Player.Instance.Move(new Vector2Int(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Player.Instance.Move(new Vector2Int(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Player.Instance.Move(new Vector2Int(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Player.Instance.Move(new Vector2Int(1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            Player.Instance.Rotate(false);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Player.Instance.Rotate(true);
        }
    }
}
