using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : SingletonBehaviour<PieceManager>
{
    public List<GameObject> pieces;
    public List<PieceGenerator> pieceGenerators;

    public int activePieceCount = 0;

    private void OnEnable()
    {
        Piece.OnPieceLand += OnPieceLand;
    }

    private void OnDisable()
    {
        Piece.OnPieceLand -= OnPieceLand;
    }

    public Piece GenerateRandomPiece()
    {
        int idx = Random.Range(0, pieces.Count);
        GameObject obj = Instantiate(pieces[idx]);
        Piece piece = obj.GetComponent<Piece>();
        return piece;
    }

    public void StartGenerating()
    {
        GenerateNext();
        Release();
    }

    public void Release()
    {
        for (int i = 0; i < pieceGenerators.Count; i++)
            pieceGenerators[i].Release();

        GenerateNext();
    }

    //todo: generate more than 1
    public void GenerateNext()
    {
        int idx = Random.RandomRange(0, pieceGenerators.Count);
        Piece piece = GenerateRandomPiece();
        pieceGenerators[idx].Spawn(piece);
        activePieceCount = 1;
    }

    public void OnPieceLand()
    {
        activePieceCount--;

        if (activePieceCount <= 0)
            Release();
    }

    public bool IsAnyPieceFalling()
    {
        return activePieceCount > 0;
    }
}
