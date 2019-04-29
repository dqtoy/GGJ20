using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CanvasManager : SingletonBehaviour<CanvasManager>
{
    public GridsManager grids;
    public Transform helpGrids;
    public GridsManager targerGrids;
    public Brush brush;
    public const float timePerCell = 0.572f;
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


    private PulseAnimation targetPulse;

    private void Awake()
    {
        targetPulse = targerGrids.GetComponent<PulseAnimation>();
    }

    public void Init(LevelData levelData, bool withHelpGrids)
    {
        width = levelData.width;
        height = levelData.height;
        grids.Init(levelData.values, false);
        grids.SetPattern(0);
        targerGrids.Init(levelData.values, true);
        helpGrids.gameObject.SetActive(withHelpGrids);

        speedModifies = new int[height];
        rowTimesTotal = new int[height];
        int totalTime = 0;
        for (int i = 0; i < height; i++)
        {
            speedModifies[i] = (Random.Range(1, 3));
            float modifiedTimePerCell = timePerCell / speedModifies[i];
            int rowScanTime = (int)(modifiedTimePerCell * (width + 1) * 1000);
            int preTotal = (i == 0) ? 0 : rowTimesTotal[i - 1];
            rowTimesTotal[i] = rowScanTime + preTotal;
            totalTime += rowScanTime;
        }
    }

    public void StartPlay()
    {
        currentRow = 0;
        currentCell = -1;
        currentRowTime = rowTimesTotal[0];
        PrepareScan();
        startTime = RhythmManager.Instance.GetNextOddBeatTime();
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
        //check finish
        if (currentRow >= height - 1)
        {
            LevelManager.Instance.CheckResult();
            return;
        }

        currentRow++;
        currentCell = currentRow * width - 1;
        currentRowTime = rowTimesTotal[currentRow] - rowTimesTotal[currentRow - 1];
        rowStartTime = rowTimesTotal[currentRow - 1];

        PrepareScan();
    }


    private void Update()
    {
        if (!scanning)
            return;

        UpdateProgress();
        UpdateCurrentCell();

        if (GameManager.Instance.debug)
        {
            if (onEmpty)
            {
                grids.SetDebugValue(currentCell, false);
            }
            else
            {
                grids.SetDebugValue(currentCell - 1, false);
                grids.SetDebugValue(currentCell, true);
            }
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
        MusicManager.Instance.PlayDraw();
    }

    public void ActionB()
    {
        if (onEmpty)
            return;

        Brush.Instance.PlayPaint();
        grids.SetValue(currentCell, 0);
        MusicManager.Instance.PlayErase();
    }

    public void UpdateProgress()
    {
        totalTimeElapse = DateTimeUtil.MillisecondsElapseFromMilliseconds(startTime);
        rowTimeElapse = totalTimeElapse - rowStartTime;
        progress = (float)rowTimeElapse / currentRowTime;

        if (progress > 1)
        {
            OnRowScanFinish();
        }
    }

    public void UpdateCurrentCell()
    {
        float halfCell = 1.0f / (width + 1);
        float adjustedProcess = progress + halfCell / 2;
        int offset = (int)(adjustedProcess * (width + 1));
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
