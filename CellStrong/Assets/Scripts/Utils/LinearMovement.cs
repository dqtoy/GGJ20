using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    public Transform root;
    public Transform p1, p2;
    public float time;

    public bool randomSpin;
    public float spinSpeed;

    private float progress = 0;
    private void Awake()
    {
        if (randomSpin)
            spinSpeed = Random.RandomRange(-5.0f, 5.0f);
        root.transform.localScale = Vector3.one * Random.RandomRange(0.5f, 2.0f);

        progress = 0;
        DOTween.To(() => progress, x => progress = x, 1, time).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        root. transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        root.transform.position = Vector3.Lerp(p1.position, p2.position, progress);
    }


}
