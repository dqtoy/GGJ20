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
}
