using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceGenStatus
{
    Empty,
    Ready
}

public class PieceGenerator : MonoBehaviour
{
    public Transform spawnRoot;
    public Piece currentPiece;

    public Vector3 direction;

    PieceGenStatus status;

    public void Spawn(Piece piece)
    {
        piece.direction = direction;
        piece.status = PieceStatus.Freeze;
        piece.transform.SetParent(spawnRoot);
        piece.transform.localPosition = Vector3.zero;
        currentPiece = piece;

        status = PieceGenStatus.Ready;
    }

    public Piece Release()
    {
        if (currentPiece == null)
            return null;

        Piece releasedPiece = currentPiece;
        currentPiece.status = PieceStatus.Moving;
        status = PieceGenStatus.Empty;
        currentPiece = null;
        return releasedPiece;
    }
}
