using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    public Transform pulseRoot;
    public float pulseScale = 1.5f;
    public float phaseTime = 0.5f;
    public bool SyncWithRhythem = false;

    private string id;
    private float scale;
    private bool playing = false;

    private void OnEnable()
    {
        if (SyncWithRhythem)
            RhythmManager.OnBGBeat += PlayEffect;
    }

    private void OnDisable()
    {
        if (SyncWithRhythem)
            RhythmManager.OnBGBeat -= PlayEffect;
    }

    private void PlayEffect()
    {
        Play();
    }

    private void Start()
    {
        id = "pulse" + System.Guid.NewGuid();

        if (pulseRoot == null)
            pulseRoot = transform;
    }

    private void Update()
    {
        if (!playing)
            scale = 1;

        pulseRoot.transform.localScale = Vector3.one * scale;
    }

    public void Play()
    {
        //Debug.Log(MusicManager.Instance.GetBGTime());
        DOTween.Kill(id);
        scale = pulseScale;
        playing = true;
        DOTween.To(() => scale, x => scale = x, 1, phaseTime).SetEase(Ease.OutCubic).SetId(id).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        playing = false;
    }
}