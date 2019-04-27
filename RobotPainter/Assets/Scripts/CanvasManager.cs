using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasManager : SingletonBehaviour<CanvasManager>
{
    public GridsManager grids;
    public const float timePerCell = 1;

    private bool scanning = false;
    private int currentRow;
    private int currentCell;
    private long rowStartTime;
    private float rowScanTime;

    private float progress = 0;
    private float valueTo = 1;


    public void Init(LevelData levelData)
    {

        currentRow = 0;
        currentCell = 0;
    }

    public void StartRow(int row)
    {
        //todo: change speed
        rowScanTime = 1;

        currentRow = row;
        rowStartTime = DateTimeUtil.GetUnixTime();
    }

    public void StartScan()
    {
        DOTween.To(() => progress, x => progress = x, 1, rowScanTime).OnComplete(OnRowScanFinish);

        scanning = true;
    }

    public void OnRowScanFinish()
    {
        //playing = false;
        //callback.Invoke();
    }


    private void Update()
    {

    }

    //paint black
    public void ActionA()
    {
        Brush.Instance.PlayPaint();
    }

    public void ActionB()
    {

    }

    public void UpdateCurrentCell()
    {
        long timeElapse = DateTimeUtil.MillisecondsElapse(LevelManager.Instance.levelStartTime);
        currentCell =(int)(timeElapse / timePerCell);
    }


}
