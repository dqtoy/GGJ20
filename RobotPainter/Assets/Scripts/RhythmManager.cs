using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : SingletonBehaviour<RhythmManager>
{
    public delegate void BGBeat();
    public static event BGBeat OnBGBeat;

    public delegate void BGBeatOdd();
    public static event BGBeatOdd OnBGBeatOdd;

    public delegate void BGBeatEven();
    public static event BGBeatEven OnBGBeatEven;

    public int beatTime;
    public int offset; 

    private int lastBeat;
    private int timeElapse;
    private int beat;

    private int lastBeatStamp;
    private int nextBeatStamp;

    public static void BGBeatOccur()
    {
        OnBGBeat();
    }

    public static void BGBeatOccurOdd()
    {
        OnBGBeatOdd();
    }

    public static void BGBeatOccurEven()
    {
        OnBGBeatEven();
    }

    public void Reset()
    {
        lastBeat = 0;
    }

    private void Update()
    {
        timeElapse = MusicManager.Instance.GetBGTime() + offset;
        beat = timeElapse / beatTime;
        if (beat > lastBeat)
        {
            lastBeat = beat;
            BGBeatOccur();

            /*
            if (beat % 2 == 1)
            {
                BGBeatOccurOdd();
            }
            else
            {
                BGBeatOccurEven();
            }
            */
        }
    }

    public long GetNextBeatTime()
    {
        long nowTime = DateTimeUtil.GetUnixTimeMilliseconds();
        timeElapse = MusicManager.Instance.GetBGTime() + offset;
        int currentBeat = timeElapse / beatTime;
        int till = (currentBeat + 1) * beatTime - timeElapse;
        return nowTime + till;
    }

    public long GetNextOddBeatTime()
    {
        long nowTime = DateTimeUtil.GetUnixTimeMilliseconds();
        timeElapse = MusicManager.Instance.GetBGTime() + offset;
        int currentBeat = timeElapse / beatTime;
        int offBeat = ((currentBeat % 2) == 0) ? 2 : 1;
        int till = (currentBeat + offBeat) * beatTime - timeElapse;
        return nowTime + till;
    }

    public int GetTillNextOddBeatTime()
    {
        long nowTime = DateTimeUtil.GetUnixTimeMilliseconds();
        timeElapse = MusicManager.Instance.GetBGTime() + offset;
        int currentBeat = timeElapse / beatTime;
        int offBeat = ((currentBeat % 2) == 0) ? 2 : 1;
        int till = (currentBeat + offBeat) * beatTime - timeElapse;
        return till;
    }

    public int GetLastBeatTime()
    {
        return 0;
    }
}