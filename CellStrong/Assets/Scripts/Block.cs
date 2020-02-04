using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Default = 0
}

public class Block : MonoBehaviour
{
    public BlockType blockType;
    public int layer;
    public SpriteRenderer sprite;

    public void UpdateLayer(int value)
    {
        Debug.Log(value);
        if (layer == value)
            return;
        layer = value;
        if (layer > 10)
            layer = 10;
        sprite.sprite = GridManager.Instance.blockSpries[layer - 1];
    }
}
