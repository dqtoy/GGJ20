using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : SingletonBehaviour<TickManager>
{
    public float fallTime;
    float previousTime;

    public delegate void Tick();
    public static event Tick OnTick;


    void Update()
    {
        if (!PieceManager.Instance.IsAnyPieceFalling())
            return;

        float time = Time.time;

        if (time - previousTime > fallTime)
        {
            OnTick();
            previousTime = time;
        }
    }
}
