using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceStatus
{
    Freeze,
    Moving
}

public class Piece : MonoBehaviour
{
    public List<Block> blocks;
    public List<Vector2Int> offset;
    public Vector3 direction;
    public PieceStatus status;

    public delegate void PieceLand();
    public static event PieceLand OnPieceLand;

    private void OnEnable()
    {
        TickManager.OnTick += OnTick;
    }

    private void OnDisable()
    {
        TickManager.OnTick -= OnTick;
    }

    private void OnTick()
    {
        if (status == PieceStatus.Freeze)
            return;

        bool canMove = NextValid();
        DebugUI.Instance.nextValid.text = canMove ? "Can Move" : "Can't Move";
        if (canMove)
        {
            Vector3 newPos = transform.localPosition + direction;
            transform.localPosition = newPos;
        }
        else  //land
        {
            for (int i = 0; i < blocks.Count; i++)
                GridManager.Instance.AddBlock(blocks[i]);
            OnPieceLand();
            Destroy(gameObject);
        }
    }

    private bool NextValid()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            Vector3 nextPos = blocks[i].transform.position + direction;
            if (!GridManager.Instance.IsBlockEmpty(nextPos))
                return false;
        }
        return true;
    }



}
