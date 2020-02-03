using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonBehaviour<Player>
{
    public Transform anchor;
    public Vector2Int centerXY;
    public Block block;

    [HideInInspector]
    public RotateAnimation rotAnim;

    public Transform rangeObj;
    public Collider2D cld;

    [Header("Debug")]
    public SpriteRenderer rangeSprite;

    private float range;
    private int animCount = 0;

    private void Start()
    {
        centerXY = GridManager.Instance.GetCoordinate(transform.position);
    }

    private void Update()
    {
        rangeObj.transform.localScale = Vector3.Lerp(rangeObj.transform.localScale, Vector3.one * range, 1.5f * Time.deltaTime);
        for (int i = 0; i < PieceManager.Instance.activePieces.Count; i++)
        {
            if (PieceManager.Instance.activePieces[i] != null
                && cld.bounds.Contains(PieceManager.Instance.activePieces[i].transform.position))
            {
                rangeSprite.color = Color.red;
                return;
            }
        }
        rangeSprite.color = Color.white;
    }

    public void Move(Vector2Int vec)
    {
        if (IsBusy() || GridManager.Instance.IsBusy())
            return;

        if (!ValideMove(vec))
            return;

        Vector3 newPos = transform.position + new Vector3(vec.x, vec.y, 0);
        transform.position = newPos;
        centerXY = GridManager.Instance.GetCoordinate(newPos);

        GridManager.Instance.Rescan();
    }

    public void Rotate(bool clockwise)
    {
        if (IsBusy() || !CanRotate() || GridManager.Instance.IsBusy())
            return;

        RotateAnimation rotateAnim = GetComponent<RotateAnimation>();
        if (rotateAnim != null)
            return;

        Vector3 rot = clockwise ? new Vector3(0, 0, 90) : new Vector3(0, 0, -90);
        rotAnim = gameObject.AddComponent<RotateAnimation>();
        PushAnimation();
        rotAnim.Rotate(rot, PopAnimation);
    }

    private bool ValideMove(Vector2Int vec)
    {
        //todo: out of boundary

        for (int i = 0; i < PieceManager.Instance.activePieces.Count; i++)
        {
            if (PieceManager.Instance.activePieces[i] == null)
                continue;
            for (int j = 0; j < PieceManager.Instance.activePieces[i].blocks.Count; j++)
            {
                Vector3 pos = PieceManager.Instance.activePieces[i].blocks[j].transform.position - new Vector3(vec.x, vec.y, 0);
                if (!GridManager.Instance.IsBlockEmpty(pos))
                {
                    PieceManager.Instance.activePieces[i].TryLand();
                    return false;
                }
            }
        }

        return true;
    }

    public void UpdateRange(float value)
    {
        range = value;
    }

    void PushAnimation()
    {
        animCount++;
    }

    void PopAnimation()
    {
        animCount--;
        GridManager.Instance.Rescan();
    }

    public bool IsBusy()
    {
        return (animCount > 0);
    }

    public bool CanRotate()
    {
        for (int i = 0; i < PieceManager.Instance.activePieces.Count; i++)
        {
            if (PieceManager.Instance.activePieces[i] != null
                && cld.bounds.Contains(PieceManager.Instance.activePieces[i].transform.position))
                return false;
        }
        return true;
    }
}
