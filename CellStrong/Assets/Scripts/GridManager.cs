using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingletonBehaviour<GridManager>
{
    public Transform origin;
    public Collider2D collider;
    public Block[,] grids = new Block[21, 21];
    private int width = 21;
    private int height = 21;

    public void Init()
    {
        Block playerBlock = Player.Instance.block;
        playerBlock.layer = 0;
        AddBlock(playerBlock, 10, 10);
    }

    public void AddBlock(Block block)
    {
        Vector2Int pos = GetCoordinate(block.transform.position);
        AddBlock(block, pos.x, pos.y);
    }

    public void AddBlock(Block block, int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.Log("Out of bound");
            return;
        }
        grids[x, y] = block;
        block.transform.SetParent(Player.Instance.anchor);
    }

    public void RemoveBlock(int x, int y)
    {
        grids[x, y] = null;
    }

    public void RemoveLayer(int layer)
    {

    }

    public void Clear()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                grids[i, j] = null;
    }

    //assume 4 unit direction only
    public void Rescan()
    {
        Clear();
        foreach (Transform child in Player.Instance.anchor)
        {
            Vector2Int pos = GetCoordinate(child.position);
            grids[pos.x, pos.y] = child.GetComponent<Block>();
        }
    }
    public void Move(Vector2Int offset)
    {
        Clear();
        foreach (Transform child in Player.Instance.anchor)
        {
            Vector2Int pos = GetCoordinate(child.position);
            grids[pos.x, pos.y] = child.GetComponent<Block>();
        }
        

        /*
        if (offset.x == 1)
        {
            for(int j = 0; j < height; j++)
            {
                for(int i = width - 1; i > 0; i--)
                {
                    grids[i, j] = grids[i - 1, j];
                }
                grids[0, j] = null;
            }
        }
        */
    }

    public bool IsEmpty(int x, int y)
    {
        return (grids[x, y] == null);
    }

    public bool IsPointInside(Vector3 point)
    {
        return collider.bounds.Contains(point);
    }

    public bool IsBlockEmpty(Vector3 point)
    {
        int x = Mathf.RoundToInt(point.x - origin.position.x);
        int y = Mathf.RoundToInt(point.y - origin.position.y);
        if (x < 0 || y < 0 || x >= width || y >= height)
            return true;

        return grids[x, y] == null;
    }

    public Vector2Int GetCoordinate(Vector3 point)
    {
        return new Vector2Int(Mathf.RoundToInt(point.x - origin.position.x), Mathf.RoundToInt(point.y - origin.position.y));
    }
}
