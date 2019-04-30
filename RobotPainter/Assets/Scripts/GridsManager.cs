using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridsManager : MonoBehaviour
{
    public List<SpriteRenderer> grids;
    public int[] values;
    public int[] targetValues;

    private int totalScore;
    private int width = 7;
    private int height = 6;

    private void Awake()
    {
        grids = new List<SpriteRenderer>();
        foreach (Transform child in transform)
        {
            grids.Add(child.GetComponent<SpriteRenderer>());
        }
    }

    public void Init(int[] targets, bool setTile = false)
    {
        values = new int[targets.Length];
        targetValues = new int[targets.Length];
        totalScore = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            targetValues[i] = targets[i];
            totalScore += targets[i];
        }

        if (setTile)
        {
            for(int i = 0; i < targets.Length; i++)
            {
                SetValue(i, targets[i]);
            }
        }
    }

    public void SetPattern(int style)
    {
        if (style == 0)
        {
            for (int i = 0; i < values.Length; i++)
            {
                SetValue(i, 0);
            }
        }
        else if (style == 1)
        {
            for (int i = 0; i < values.Length; i++)
            {
                SetValue(i, 1);
            }
        }
        else if (style == 2)
        {
            bool flip = true;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    SetValue(i * width + j, flip ? 1 : 0);
                }
                flip = !flip;
            }
        }
    }

    public void SetValue(int idx, int value = 1)
    {
        if (idx >= values.Length)
        {
            Debug.LogError("index exceeds");
            return;
        }

        values[idx] = value;
        //Color newC = (value == 1) ? new Color(0, 0, 0, 1) : new Color(1, 1, 1, 1);
        //grids[idx].GetComponent<SpriteRenderer>().color = newC;
        grids[idx].enabled = value == 1;
    }

    public void SetDebugValue(int idx, bool on)
    {
        if (idx < 0 || idx >= grids.Count)
            return;

        //grids[idx].GetComponent<SpriteRenderer>().color = on ? new Color(1, 0, 0, 1) : new Color(1, 1, 1, 1);
        grids[idx].enabled = on;
    }

    public Transform GetTile(int idx)
    {
        if (idx >= values.Length)
        {
            Debug.LogError("index exceeds");
            return null;
        }

        return grids[idx].transform;
    }

    public int GetScore()
    {
        int score = 0;
        for(int i = 0; i < values.Length; i++)
        {
            if (targetValues[i] == 1 && values[i] == 1)
                score++;

            if ((targetValues[i] == 1 && values[i] == 0) ||
            (targetValues[i] == 0 && values[i] == 1))
                score--;
        }

        return score;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
