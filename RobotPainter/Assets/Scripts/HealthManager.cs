using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : SingletonBehaviour<HealthManager>
{
    public Text healthText;
    public int health;

    private void Update()
    {
        healthText.text = health.ToString();
    }

    public void SetHealth(int value)
    {
        health = value;
        healthText.text = health.ToString();
    }

    public void AddHealth(int value)
    {
        health += value;
        healthText.text = health.ToString();
    }
}
