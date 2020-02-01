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
    public Vector2 speed;
    public PieceStatus status;

    public delegate void PieceLand();
    public static event PieceLand OnPieceLand;

}
