using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingletonBehaviour<GridManager>
{
    public Transform origin;
    public Collider2D cld;
    public List<Sprite> blockSpries = new List<Sprite>();
    public Block[,] grids = new Block[101, 101];
    private int width = 101;
    private int height = 101;

    private int animCount = 0;
    private bool needRecheck = false;

    public void Init()
    {
        Block playerBlock = Player.Instance.block;
        playerBlock.layer = 0;
        AddBlock(playerBlock, 10, 10);
        Rescan();
    }

    private void Update()
    {
        if (needRecheck && animCount == 0)
        {
            TryClear();
            needRecheck = false;
        }
    }

    public void AddBlock(Block block)
    {
        Vector2Int pos = GetCoordinate(block.transform.position);
        AddBlock(block, pos.x, pos.y);
    }

    public void AddBlock(Block block, int x, int y)
    {
        if (!XYValid(x, y))
        {
            Debug.Log("Out of bound");
            return;
        }
        block.UpdateLayer(Mathf.Max(Mathf.Abs(Player.Instance.centerXY.x - x), Mathf.Abs(Player.Instance.centerXY.y - y)));
        grids[x, y] = block;
        block.transform.SetParent(Player.Instance.anchor);
    }

    public void RemoveBlock(Block block)
    {
        if (block == null)
            return;

        //todo: play particle effect
        Vector2Int pos = GetCoordinate(block.transform.position);
        RemoveBlock(pos.x, pos.y);
    }

    public void RemoveBlock(int x, int y)
    {
        //todo: play particle effect
        if (grids[x, y] != null)
            Destroy(grids[x, y].gameObject);
    }

    public void FallAt(Vector2Int xy, Vector2Int dir, Action callback = null)
    {
        int x = xy.x - dir.x;
        int y = xy.y - dir.y;
        float delay = 0.0f;
        int newLayer = 0;

        while (x >= 0 && x < width && y >= 0 && y < height)
        {
            if (grids[x, y] != null)
            {
                //grids[x, y].transform.position = origin.position + new Vector3(x - dir.x, y - dir.y, 0);
                Block block = grids[x, y];
                newLayer = Mathf.Max(Mathf.Abs(x + dir.x - Player.Instance.centerXY.x), Mathf.Abs(y + dir.y - Player.Instance.centerXY.y));

                TranslateAnimation fallAnim = block.gameObject.AddComponent<TranslateAnimation>();
                PushAnimation();
                fallAnim.TranslateTo(origin.position + new Vector3(x + dir.x, y + dir.y, 0), delay, ()=>
                {
                    block.ShiftLayer(-1);
                    PopAnimation();
                });
            }
            grids[x + dir.x, y + dir.y] = grids[x, y];
            x = x - dir.x;
            y = y - dir.y;
            delay += 0.1f;
        }
        grids[x + dir.x, y + dir.y] = null;


        if (callback != null)
            callback();
    }

    public void TryClear()
    {
        int x = Player.Instance.centerXY.x;
        int y = Player.Instance.centerXY.y;
        List<Block> toTeRemoved = new List<Block>();
        List<Tuple<Vector2Int, Vector2Int>> toFallOff = new List<Tuple<Vector2Int, Vector2Int>>();
        for (int i = 8; i > 0; i--)
        {
            bool full1 = true, full2 = true, full3 = true, full4 = true;
            //up line
            for (int j = 0; j < i * 3; j++)
            {
                if (!XYValid(x - i + j, y + i) || grids[x - i + j, y + i] == null)
                {
                    full1 = false; break;
                }
            }
            if (full1)
            {
                for (int j = 0; j < i * 3; j++)
                {
                    toTeRemoved.Add(grids[x - i + j, y + i]);
                    toFallOff.Add(Tuple.Create(new Vector2Int(x - i + j, y + i), new Vector2Int(0, -1)));
                }
            }

            //bottom line
            for (int j = 0; j < i * 3; j++)
            {
                if (!XYValid(x - i + j, y - i) || grids[x - i + j, y - i] == null)
                {
                    full2 = false; break;
                }
            }
            if (full2)
            {
                for (int j = 0; j < i * 3; j++)
                {
                    toTeRemoved.Add(grids[x - i + j, y - i]);
                    toFallOff.Add(Tuple.Create(new Vector2Int(x - i + j, y - i), new Vector2Int(0, 1)));
                }
            }

            //left line
            for (int j = 0; j < i * 3; j++)
            {
                if (!XYValid(x - i, y - i + j) || grids[x - i, y - i + j] == null)
                {
                    full3 = false; break;
                }
            }
            if (full3)
            {
                for (int j = 0; j < i * 3; j++)
                {
                    toTeRemoved.Add(grids[x - i, y - i + j]);
                    toFallOff.Add(Tuple.Create(new Vector2Int(x - i, y - i + j), new Vector2Int(1, 0)));
                }
            }

            //right line
            for (int j = 0; j < i * 3; j++)
            {
                if (!XYValid(x + i, y - i + j) || grids[x + i, y - i + j] == null)
                {
                    full4 = false; break;
                }
            }
            if (full4)
            {
                for (int j = 0; j < i * 3; j++)
                {                    
                    toTeRemoved.Add(grids[x + i, y - i + j]);
                    toFallOff.Add(Tuple.Create(new Vector2Int(x + i, y - i + j), new Vector2Int(-1, 0)));
                }
            }

            if (toTeRemoved.Count > 0)
            {
                //remove marked
                for (int k = 0; k < toTeRemoved.Count; k++)
                {
                    RemoveBlock(toTeRemoved[k]);
                }

                //fall at marked
                //todo: on destroy effect complete
                for (int k = 0; k < toFallOff.Count; k++)
                {
                    FallAt(toFallOff[k].Item1, toFallOff[k].Item2);
                }
                needRecheck = true;
                break;
            }
        }
    }

    public void RemoveLayer(int layer)
    {

    }

    public void ClearAll()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                grids[i, j] = null;
    }

    //assume 4 unit direction only
    public void Rescan()
    {
        ClearAll();
        Vector2Int pos;
        float maxDist = 0;
        foreach (Transform child in Player.Instance.anchor)
        {
            pos = GetCoordinate(child.position);
            maxDist = Mathf.Max(maxDist, Vector2Int.Distance(pos, Player.Instance.centerXY));
            grids[pos.x, pos.y] = child.GetComponent<Block>();
        }
        Player.Instance.UpdateRange(maxDist + 1);
    }
    public void Move(Vector2Int offset)
    {
        ClearAll();
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

    public void Shfit()
    {

    }

    public bool IsEmpty(int x, int y)
    {
        return (grids[x, y] == null);
    }

    public bool IsPointInside(Vector3 point)
    {
        return cld.bounds.Contains(point);
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

    public bool XYValid(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < width && y < height);
    }

    void PushAnimation()
    {
        animCount++;
    }

    void PopAnimation()
    {
        animCount--;
    }

    public bool IsBusy()
    {
        return (animCount > 0);
    }


    void OnDrawGizmosSelected()
    {

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (grids[i, j] != null)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(grids[i, j].transform.position, 0.5f);
                }
            }
    }
}
