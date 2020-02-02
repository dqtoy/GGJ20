using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    public void SpawnPlayer()
    {
        GridManager.Instance.Init();
    }
}
