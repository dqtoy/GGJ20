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
        TranslateTo(position, 0, callback);
    }

    public void TranslateTo(Vector3 position, float delay, Action callback = null)
    {
        this.callback = callback;
        PlayTranslate(position, delay, false);
    }

    public void Translate(Vector3 position, float delay = 0, Action callback = null)
    {
        this.callback = callback;
        PlayTranslate(position, delay, true);
    }

    public void ScaleTo(Vector3 targetScale, Action callback = null)
    {
        this.callback = callback;
        PlayScale(targetScale, false);
    }

    private void PlayTranslate(Vector3 position, float delay, bool relative)
    {
        transform.DOMove(position, 0.3f).SetRelative(relative).SetDelay(delay).OnComplete(OnFinish);
        //transform.DOJump(position, 1, 1, 0.3f).SetRelative(relative).OnComplete(OnFinish);
        //Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(transform.DOLocalMoveY(0.5f, 0.3f).SetRelative(true));
        //mySequence.Append(transform.DOLocalMoveY(-0.5f, 0.3f).SetRelative(true).OnComplete(OnFinish));
    }

    private void PlayScale(Vector3 targetScale, bool relative)
    {
        transform.DOScale(targetScale, 0.3f).SetRelative(relative).OnComplete(OnFinish);
    }

    private void OnFinish()
    {
        if (callback != null)
            callback();

        Destroy(this);
    }
}
