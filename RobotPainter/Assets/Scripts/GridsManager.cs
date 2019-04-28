using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridsManager : MonoBehaviour
{
    public List<Transform> grids;
    public int[] gridvalues;

    private void Awake()
    {
        grids = new List<Transform>();
        foreach (Transform child in transform)
        {
            grids.Add(child);
        }
    }

    public void Init(int[] values, bool setTile = false)
    {
        gridvalues = new int[values.Length];
        for (int i = 0; i < values.Length; i++)
            gridvalues[i] = values[i];

        if (setTile)
        {
            for(int i = 0; i < values.Length; i++)
            {
                if (values[i] == 1)
                    SetValue(i);
            }
        }
    }

    public void SetValue(int idx, int value = 1)
    {
        if (idx >= gridvalues.Length)
        {
            Debug.LogError("index exceeds");
            return;
        }

        gridvalues[idx] = value;
        grids[idx].GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
    }

    public void SetDebugValue(int idx, bool on)
    {
        if (idx < 0)
            return;

        grids[idx].GetComponent<SpriteRenderer>().color = on ? new Color(1, 0, 0, 1) : new Color(1, 1, 1, 1);
    }

    public Transform GetTile(int idx)
    {
        if (idx >= gridvalues.Length)
        {
            Debug.LogError("index exceeds");
            return null;
        }

        return grids[idx];
    }
}
