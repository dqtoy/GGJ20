using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridsManager : MonoBehaviour
{
    public List<Transform> grids;
    public int[] values;
    public int[] targetValues;

    private void Awake()
    {
        grids = new List<Transform>();
        foreach (Transform child in transform)
        {
            grids.Add(child);
        }
    }

    public void Init(int[] targets, bool setTile = false)
    {
        values = new int[targets.Length];
        targetValues = new int[targets.Length];

        for (int i = 0; i < targets.Length; i++)
            targetValues[i] = targets[i];

        if (setTile)
        {
            for(int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == 1)
                    SetValue(i);
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
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    SetValue(i * 7 + j, flip ? 1 : 0);
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
        Color newC = (value == 1) ? new Color(0, 0, 0, 1) : new Color(1, 1, 1, 1);
        grids[idx].GetComponent<SpriteRenderer>().color = newC;
    }

    public void SetDebugValue(int idx, bool on)
    {
        if (idx < 0 || idx >= grids.Count)
            return;

        grids[idx].GetComponent<SpriteRenderer>().color = on ? new Color(1, 0, 0, 1) : new Color(1, 1, 1, 1);
    }

    public Transform GetTile(int idx)
    {
        if (idx >= values.Length)
        {
            Debug.LogError("index exceeds");
            return null;
        }

        return grids[idx];
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
}
