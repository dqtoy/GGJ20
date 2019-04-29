using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int id;
    public int width, height;
    public int[] values;
}

public class LevelService : SingletonBehaviour<LevelService>
{
    public List<LevelData> levels;

    private void Awake()
    {
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


    }

    public LevelData GetLevel(int currentLevel)
    {
        int randomIdx = currentLevel;
        while(randomIdx == currentLevel)
        {
            randomIdx = Random.Range(0, levels.Count);
        }
        return levels[randomIdx];
    }
}
