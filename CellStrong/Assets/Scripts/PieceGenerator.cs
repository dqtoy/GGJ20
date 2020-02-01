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


    PieceGenStatus status;

    public void Spawn(Piece piece)
    {
        piece.status = PieceStatus.Freeze;
        piece.transform.SetParent(spawnRoot);
        currentPiece = piece;

        status = PieceGenStatus.Ready;
    }

    public void Release()
    {
        if (currentPiece == null)
            return;

        currentPiece.status = PieceStatus.Moving;
        status = PieceGenStatus.Empty;
    }
}
