using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public const int None = 0;
    public const int King = 1;
    public const int Pawn = 2;
    public const int Knight = 3;
    public const int Bishop = 5;
    public const int Rook = 6;
    public const int Queen = 7;

    public const int White = 8;
    public const int Black = 16;
    
    private SpriteRenderer spriteR;

    public Sprite whiteKing;
    public Sprite whiteQueen;
    public Sprite whiteRook;
    public Sprite whiteBishop;
    public Sprite whiteKnight;
    public Sprite whitePawn;
    public Sprite blackKing;
    public Sprite blackQueen;
    public Sprite blackRook;
    public Sprite blackBishop;
    public Sprite blackKnight;
    public Sprite blackPawn;


    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
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
