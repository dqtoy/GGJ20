using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CanvasManager : SingletonBehaviour<CanvasManager>
{
    public GridsManager grids;
    public GridsManager targerGrids;
    public Brush brush;
    public const float timePerCell = 2f;
    public const float cellWidth = 1;

    public Text debugText;

    public int width, height;
    private bool scanning = false;
    private int currentRow;
    private int currentCell;
    private Vector3 brushFrom;
    private Vector3 brushTo;
    private long startTime;
    private int totalTimeElapse;
    private int rowTimeElapse;
    private int rowStartTime;
    private float progress = 0;

    private bool onEmpty = false;

    private int[] speedModifies;
    private int[] rowTimesTotal;
    private int currentRowTime;


    public void Init(LevelData levelData)
    {
        width = levelData.width;
        height = levelData.height;
        grids.Init(levelData.values);
        targerGrids.Init(levelData.values, true);

        speedModifies = new int[height];
        rowTimesTotal = new int[height];
        int totalTime = 0;
        for (int i = 0; i < height; i++)
        {
            speedModifies[i] = (Random.Range(1, 3));
            float modifiedTimePerCell = timePerCell / speedModifies[currentRow];
            float rowScanTime = modifiedTimePerCell * (width + 1);
            rowTimesTotal[i] = (int)(rowScanTime * 1000) + totalTime;
            totalTime += rowTimesTotal[i];
        }
    }

    public void StartPlay()
    {
        currentRow = 0;
        currentCell = -1;
        currentRowTime = rowTimesTotal[0];
        PrepareScan();
        startTime = DateTimeUtil.GetUnixTime();
        rowStartTime = 0;
        scanning = true;
    }

    public void PrepareScan()
    {
        //move brush
        Transform startTile = grids.GetTile(currentCell + 1);
        Transform endTile = grids.GetTile(currentCell + width);
        brushFrom = startTile.position - new Vector3(0.5f, 0, 0);
        brushTo = endTile.position + new Vector3(0.5f, 0, 0);
        brush.transform.position = brushFrom;

        StartScan();
    }

    public void StartScan()
    {
        progress = 0;
    }

    public void OnRowScanFinish()
    {
        currentRow++;
        currentCell = currentRow * width - 1;
        currentRowTime = rowTimesTotal[currentRow] - rowTimesTotal[currentRow - 1];
        rowStartTime = rowTimesTotal[currentRow - 1];

        if (currentRow >= height)
        {
            LevelManager.Instance.CheckResult();
            return;
        }

        PrepareScan();
    }


    private void Update()
    {
        if (!scanning)
            return;

        UpdateProgress();
        UpdateCurrentCell();


        if (onEmpty)
        {
            grids.SetDebugValue(currentCell, false);
        }
        else
        {
            grids.SetDebugValue(currentCell - 1, false);
            grids.SetDebugValue(currentCell, true);
        }


        brush.transform.position = Vector3.Lerp(brushFrom, brushTo, progress);
    }

    //paint black
    public void ActionA()
    {
        if (onEmpty)
            return;

        Brush.Instance.PlayPaint();
        grids.SetValue(currentCell);
    }

    public void ActionB()
    {

    }

    public void UpdateProgress()
    {
        totalTimeElapse = DateTimeUtil.MillisecondsElapse(startTime);
        rowTimeElapse = totalTimeElapse - rowStartTime;
        progress = (float)rowTimeElapse / currentRowTime;

        if (progress > 1)
        {
            OnRowScanFinish();
        }
    }

    public void UpdateCurrentCell()
    {
        int offset = (int)(progress * (width + 1));
        currentCell = offset + currentRow * width - 1;
        if (offset < 1)
        {
            onEmpty = true;
            debugText.text = "empty" + "---" + rowTimeElapse.ToString();
        }
        else if (offset > width)
        {
            onEmpty = true;
            currentCell--;
            debugText.text = "empty" + "---" + rowTimeElapse.ToString();
        }
        else
        {
            onEmpty = false;
            debugText.text = currentCell.ToString() + "---" + rowTimeElapse.ToString();
        }
    }


}
