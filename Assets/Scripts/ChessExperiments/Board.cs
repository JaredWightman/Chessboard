using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public BoardTheme boardTheme;
    public bool whiteIsBottom = true;

    MeshRenderer[, ] squareRenderers;
    SpriteRenderer[, ] squarePieceRenderers;

    const float pieceDepth = -0.1f;
    const float pieceDragDepth = -0.2f;

    public Color lightColor;
    public Color darkColor;

    public Sprite whiteKing, whiteQueen, whiteRook, whiteBishop, whiteKnight, whitePawn;
    public Sprite blackKing, blackQueen, blackRook, blackBishop, blackKnight, blackPawn;

    public const int None = 0;
    public const int King = 1;
    public const int Pawn = 2;
    public const int Knight = 3;
    public const int Bishop = 5;
    public const int Rook = 6;
    public const int Queen = 7;

    public const int White = 8;
    public const int Black = 16;
    
    void Awake()
    {
        CreateBoardUI();
    }

    void CreateBoardUI()
    {
        Shader squareShader = Shader.Find ("Unlit/Color");
        squareRenderers = new MeshRenderer[8, 8];
        squarePieceRenderers = new SpriteRenderer[8, 8];

        for ( int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                Transform square = GameObject.CreatePrimitive (PrimitiveType.Quad).transform;
                square.parent = transform;
                square.position = PositionFromCoord (file, rank, 0);
                Material squareMatierial = new Material (squareShader);

                squareRenderers[file, rank] = square.gameObject.GetComponent<MeshRenderer> ();
                squareRenderers[file, rank].material = squareMatierial;

                SpriteRenderer pieceRenderer = new GameObject ("Piece").AddComponent<SpriteRenderer> ();
                pieceRenderer.transform.parent = square;
                pieceRenderer.transform.position = PositionFromCoord (file, rank, pieceDepth);
                pieceRenderer.transform.localScale = Vector3.one * 60 / (200 / 6f);
                squarePieceRenderers[file, rank] = pieceRenderer;
                squarePieceRenderers[file, rank].sprite = GetPieceSprite (Rook, White);

                squareRenderers[file, rank].material.color = ((file + rank) % 2 != 0) ? lightColor : darkColor;
            }
        }
    }

    public Vector3 PositionFromCoord (int file, int rank, float depth = 0)
    {
        return new Vector3 (-3.5f + file, -3.5f + rank, depth);
    }

    public Sprite GetPieceSprite (int piece, int color)
    {
        switch (piece) {
            case Pawn:
                if (color == White) {
                    return whitePawn;
                } else {
                    return blackPawn;
                }
            case Rook:
                if (color == White) {
                    return whiteRook;
                } else {
                    return blackRook;
                }
            case Knight:
                if (color == White) {
                    return whiteKnight;
                } else {
                    return blackKnight;
                }
            case Bishop:
                if (color == White) {
                    return whiteBishop;
                } else {
                    return blackBishop;
                }
            case Queen:
                if (color == White) {
                    return whiteQueen;
                } else {
                    return blackQueen;
                }
            case King:
                if (color == White) {
                    return whiteKing;
                } else {
                    return blackKing;
                }
            default:
                return null;
        }
    }
}
