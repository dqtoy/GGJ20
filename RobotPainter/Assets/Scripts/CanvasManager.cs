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
    public const float timePerCell = 0.25f;
    public const float cellWidth = 1;

    public Text debugText;

    public int width, height;
    private bool scanning = false;
    private int currentRow;
    private int currentCell;
    private long rowStartTime;
    private float rowScanTime;
    private Vector3 brushFrom;
    private Vector3 brushTo;
    private float progress = 0;

    private float modifiedTimePerCell = 1;
    private bool onEmpty = false;


    public void Init(LevelData levelData)
    {
        width = levelData.width;
        height = levelData.height;
        grids.Init(levelData.values);
        targerGrids.Init(levelData.values, true);
    }

    public void StartPlay()
    {
        currentRow = 0;
        currentCell = -1;
        PrepareScan();
    }

    public void PrepareScan()
    {
        //move brush
        Transform startTile = grids.GetTile(currentCell + 1);
        Transform endTile = grids.GetTile(currentCell + width);
        brushFrom = startTile.position - new Vector3(0.5f, 0, 0);
        brushTo = endTile.position + new Vector3(0.5f, 0, 0);
        brush.transform.position = brushFrom;

        //todo: change speed
        modifiedTimePerCell = timePerCell / Random.Range(1, 3);
        rowScanTime = modifiedTimePerCell * (width + 1);

        StartScan();
    }

    public void StartScan()
    {
        progress = 0;
        DOTween.To(() => progress, x => progress = x, 1, rowScanTime).SetEase(Ease.Linear).OnComplete(OnRowScanFinish);
        rowStartTime = DateTimeUtil.GetUnixTime();
        scanning = true;
    }

    public void OnRowScanFinish()
    {
        scanning = false;
        currentRow++;
        currentCell = currentRow * width - 1;

        //brush reset;

        if (currentRow >= height)
        {
            LevelManager.Instance.CheckResult();
            return;
        }

        PrepareScan();
    }


    private void Update()
    {
        UpdateCurrentCell();

        /*
        if (onEmpty)
        {
            grids.SetDebugValue(currentCell, false);
        }
        else
        {
            grids.SetDebugValue(currentCell - 1, false);
            grids.SetDebugValue(currentCell, true);
        }
        */

        if (scanning)
        {
            brush.transform.position = Vector3.Lerp(brushFrom, brushTo, progress);
        }
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

    public void UpdateCurrentCell()
    {
        float timeElapse = (DateTimeUtil.MillisecondsElapse(rowStartTime)) / 1000.0f;
        int offset = (int)(timeElapse / modifiedTimePerCell);
        currentCell = offset + currentRow * width - 1;
        if (offset < 1)
        {
            onEmpty = true;
            debugText.text = "empty" + "---" + timeElapse.ToString();
        }
        else if (offset > width)
        {
            onEmpty = true;
            currentCell--;
            debugText.text = "empty" + "---" + timeElapse.ToString();
        }
        else
        {
            onEmpty = false;
            debugText.text = currentCell.ToString() + "---" + timeElapse.ToString();
        }
    }


}
