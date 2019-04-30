﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int id;
    public int width, height;
    public int[] values;
    public bool flip;
}

public class LevelService : SingletonBehaviour<LevelService>
{
    public List<LevelData> levels;
    public LevelData OK;
    public LevelData NO;
    public LevelData Checking;

    private void Awake()
    {
        LoadSpecial();
        levels = new List<LevelData>();

        LevelData level = new LevelData();
        level.id = 0;
        level.width = 7;
        level.height = 6;
        level.values = new int[42] {
        0,0,0,0,0,0,0,
        0,1,1,0,1,1,0,
        0,0,1,0,1,0,0,
        0,0,0,1,0,0,0,
        0,1,1,1,1,1,0,
        0,0,0,0,0,0,0
        };
        levels.Add(level);

        level = new LevelData();
        level.id = 1;
        level.width = 7;
        level.height = 6;
        level.values = new int[42] {
        0,0,0,0,0,0,0,
        0,1,1,0,1,1,0,
        0,1,1,0,1,1,0,
        0,0,0,1,0,0,0,
        1,0,0,0,0,0,1,
        0,1,1,1,1,1,0
        };
        levels.Add(level);

        level = new LevelData();
        level.id = 2;
        level.width = 7;
        level.height = 6;
        level.values = new int[42] {
        0,0,1,0,1,0,0,
        0,1,1,1,1,1,0,
        0,1,1,0,1,1,0,
        0,0,0,1,0,0,0,
        0,0,1,1,0,0,0,
        1,1,1,1,1,1,1
        };
        levels.Add(level);

        level = new LevelData();
        level.id = 3;
        level.width = 7;
        level.height = 6;
        level.values = new int[42] {
        0,0,0,0,0,0,0,
        0,1,1,1,1,1,0,
        0,0,1,0,1,0,0,
        0,0,0,0,0,0,0,
        0,1,0,1,0,1,0,
        0,0,1,1,1,0,0
        };
        levels.Add(level);

        level = new LevelData();
        level.id = 4;
        level.width = 7;
        level.height = 6;
        level.values = new int[42] {
        1,1,1,1,1,1,1,
        1,0,0,1,0,0,1,
        1,1,1,1,1,1,1,
        1,1,1,0,1,1,1,
        1,1,0,0,0,1,1,
        1,1,1,1,1,1,1
        };
        levels.Add(level);

        level = new LevelData();
        level.id = 5;
        level.width = 7;
        level.height = 6;
        level.flip = true;
        level.values = new int[42] {
        0,0,0,0,0,0,0,
        0,1,0,0,0,1,0,
        0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,
        0,0,1,1,1,0,0,
        0,0,0,0,0,0,0
        };
        levels.Add(level);

        level = new LevelData();
        level.id = 6;
        level.width = 7;
        level.height = 6;
        level.values = new int[42] {
        1,1,1,1,1,1,1,
        1,0,0,1,0,0,1,
        1,1,1,1,1,1,1,
        1,1,0,0,0,1,1,
        1,1,1,1,1,1,1,
        1,1,1,1,1,1,1
        };
        levels.Add(level);
    }

    private void LoadSpecial()
    {
        OK = new LevelData();
        OK.id = 0;
        OK.width = 7;
        OK.height = 6;
        OK.values = new int[42] {
        0,1,0,0,0,1,0,
        1,0,1,0,1,0,1,
        1,0,0,1,0,0,1,
        0,1,0,0,0,1,0,
        0,0,1,0,1,0,0,
        0,0,0,1,0,0,0
        };

        NO = new LevelData();
        NO.id = 0;
        NO.width = 7;
        NO.height = 6;
        NO.values = new int[42] {
        1,0,0,0,0,1,0,
        0,1,0,0,1,0,0,
        0,0,1,1,0,0,0,
        0,0,1,1,0,0,0,
        0,1,0,0,1,0,0,
        1,0,0,0,0,1,0
        };

        Checking = new LevelData();
        Checking.id = 0;
        Checking.width = 7;
        Checking.height = 6;
        Checking.values = new int[42] {
        0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,
        1,0,1,0,1,0,1,
        0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,
        };
    }

    public LevelData GetLevel(int currentLevel)
    {
        int randomIdx = currentLevel;
        while(randomIdx == currentLevel)
        {
            int max = levels.Count;
            if (currentLevel == -1)
                max = 4;

            randomIdx = Random.Range(0, max);
        }
        return levels[randomIdx];
    }



    public int GetPatternByLevel(int level)
    {
        int pattern = 0;

        if (level < 6)
            pattern = 0;
        else if (level < 10)
        {
            pattern = 1;
        }
        else if (level < 15)
        {
            pattern = Random.Range(1, 3);
        }
        else
        {
            pattern = Random.Range(0, 3);
        }

        return pattern;
    }
}
