using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyManager : SingletonBehaviour<ButterFlyManager>
{
    public Transform spawnPoint;
    public GameObject butterFlyPrefab;

    public List<string> animationClips;

    private float initialDelay;

    private void Awake()
    {
        initialDelay = Random.Range(0.0f, 10.0f);
        initialDelay = 0;
    }

    public void StartSpawn(int level)
    {
        if (level < 3)
            return;

        if (level < 10)
        {
            if (Random.Range(0, 2) == 1)
                return;

            Invoke("Spawn1", initialDelay);
            return;
        }
        else if(level < 20)
        {
            if (Random.Range(0, 2) == 1)
                return;
            Invoke("Spawn1", initialDelay);
            Invoke("Spawn1", initialDelay * 1.5f);
            return;
        }
        else
        {
            if (Random.Range(0, 2) == 1)
                return;
            Invoke("Spawn1", initialDelay);
            Invoke("Spawn1", initialDelay * 2f);
            return;
        }
    }

    public void Spawn1()
    {
        GameObject butterFly = Instantiate(butterFlyPrefab, spawnPoint);
        Animator animator = butterFly.GetComponent<Animator>();
        if (animator != null)
        {
            int randomIdx = Random.Range(0, animationClips.Count);
            string clipName = animationClips[randomIdx];
            animator.Play(clipName);
        }
    }
}
