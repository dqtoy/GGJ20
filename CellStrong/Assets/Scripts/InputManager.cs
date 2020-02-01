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

    }

    private void PostGameInput()
    {

    }

    private void InGameInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
        }
    }
}
