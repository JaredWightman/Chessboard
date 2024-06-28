//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System.Diagnostics.Tracing;

public class Square
{
    private int file;
    private int rank;

    /*public const int None = 0;
    public const int King = 1;
    public const int Pawn = 2;
    public const int Knight = 3;
    public const int Bishop = 5;
    public const int Rook = 6;
    public const int Queen = 7;

    public const int White = 8;
    public const int Black = 16;*/

    private int pieceType;
    private int pieceColor;
    public Square()
    {
        /*pieceType = Piece.None;
        pieceColor = Piece.White;
        file = 0;
        rank = 0;*/
    }

    public Square(int newFile, int newRank, int type, int color)
    {
        file = newFile;
        rank = newRank;
        pieceType = type;
        pieceColor = color;
    }


    public int GetFile()
    {
        return file;
    }

    public void SetFile(int newFile)
    {
        file = newFile;
    }

    public int GetRank()
    {
        return rank;
    }

    public void SetRank(int newRank)
    {
        rank = newRank;
    }

    public int GetPiece()
    {
        return pieceType;
    }

    public void SetPiece(int type)
    {
        pieceType = type;
    }

    public int GetColor()
    {
        return pieceColor;
    }

    public void SetColor(int color)
    {
        pieceColor = color;
    }

    public void SetPieceAndColor(int type, int color)
    {
        pieceType = type;
        pieceColor = color;
    }

    public void SetEmpty()
    {
        pieceType = Piece.None;
    }
}
