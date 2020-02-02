using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonBehaviour<Player>
{
    public Transform anchor;
    public Block block;
    public Vector2 pos;
    public void Move(Vector2Int vec)
    {
        if (!ValideMove(vec))
            return;

        Vector3 newPos = transform.position + new Vector3(vec.x, vec.y, 0);
        transform.position = newPos;

        GridManager.Instance.Rescan();
    }

    public void Rotate(bool clockwise)
    {
        RotateAnimation rotateAnim = GetComponent<RotateAnimation>();
        if (rotateAnim != null)
            return;

        Vector3 rot = clockwise ? new Vector3(0, 0, 90) : new Vector3(0, 0, -90);
        rotateAnim = gameObject.AddComponent<RotateAnimation>();
        rotateAnim.Rotate(rot, null);

        GridManager.Instance.Rescan();
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
                    return false;
            }
        }

        return true;
    }
}
