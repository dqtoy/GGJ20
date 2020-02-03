using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TranslateAnimation : MonoBehaviour
{
    private Action callback;

    public void TranslateTo(Vector3 position, Action callback = null)
    {
        this.callback = callback;
        Play(position, false);
    }

    public void Translate(Vector3 position, Action callback = null)
    {
        this.callback = callback;
        Play(position, true);
    }

    private void Play(Vector3 position, bool relative)
    {
        transform.DOMove(position, 0.3f).SetRelative(relative).OnComplete(OnFinish);
        //transform.DOLocalJump(position, 1, 1, 0.1f).SetRelative(relative).OnComplete(OnFinish);
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
