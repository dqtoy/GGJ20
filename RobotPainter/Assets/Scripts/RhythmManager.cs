using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : SingletonBehaviour<RhythmManager>
{
    public delegate void BGBeat();
    public static event BGBeat OnBGBeat;

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

    public int GetLastBeatTime()
    {
        return 0;
    }
}