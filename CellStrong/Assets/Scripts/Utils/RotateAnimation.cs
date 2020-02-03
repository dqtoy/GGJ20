using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    private Action callback;

    public void RotateTo(Vector3 rotation, Action callback)
    {
        this.callback = callback;
        Play(rotation, false);
    }

    public void Rotate(Vector3 rotation, Action callback)
    {
        this.callback = callback;
        Play(rotation, true);
    }

    private void Play(Vector3 rotation, bool relative)
    {
        transform.DOLocalRotate(rotation, 0.1f).SetRelative(relative).OnComplete(OnFinish);
        //Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(transform.DOLocalMoveY(0.5f, 0.3f).SetRelative(true));
        //mySequence.Append(transform.DOLocalMoveY(-0.5f, 0.3f).SetRelative(true).OnComplete(OnFinish));
    }

    private void OnFinish()
    {
        if (callback != null)
            callback();

        Destroy(this);
    }
}