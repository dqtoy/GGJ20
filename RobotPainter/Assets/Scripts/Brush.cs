using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : SingletonBehaviour<Brush>
{
    public PulseAnimation effect;

    public void PlayPaint()
    {
        effect.Play();
    }
}
