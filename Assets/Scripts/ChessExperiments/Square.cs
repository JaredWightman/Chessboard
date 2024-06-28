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


    public int getFile()
    {
        return file;
    }

    public void setFile(int newFile)
    {
        file = newFile;
    }

    public int getRank()
    {
        return rank;
    }

    public void setRank(int newRank)
    {
        rank = newRank;
    }

    public int getPiece()
    {
        return pieceType;
    }

    public void setPiece(int type)
    {
        pieceType = type;
    }

    public int getColor()
    {
        return pieceColor;
    }

    public void setColor(int color)
    {
        pieceColor = color;
    }

    public void setPieceAndColor(int type, int color)
    {
        pieceType = type;
        pieceColor = color;
    }

    public void setEmpty()
    {
        pieceType = Piece.None;
    }
}
