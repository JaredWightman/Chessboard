/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;*/

using System.Collections.Generic;
using System.Linq;

public class Board
{
    public Square[, ] squares;

    public int colorToMove = Piece.White;

    public Board()
    {
        SetBoard();
    }

    public void SetBoard()
    {
        squares = new Square[8, 8] {
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()}
        };

        squares[0, 0].SetPieceAndColor(Piece.Rook, Piece.White);
        squares[1, 0].SetPieceAndColor(Piece.Knight, Piece.White);
        squares[2, 0].SetPieceAndColor(Piece.Bishop, Piece.White);
        squares[3, 0].SetPieceAndColor(Piece.Queen, Piece.White);
        squares[4, 0].SetPieceAndColor(Piece.King, Piece.White);
        squares[5, 0].SetPieceAndColor(Piece.Bishop, Piece.White);
        squares[6, 0].SetPieceAndColor(Piece.Knight, Piece.White);
        squares[7, 0].SetPieceAndColor(Piece.Rook, Piece.White);

        for (int i = 0; i < 8; i++) {
            squares[i, 1].SetPieceAndColor(Piece.Pawn, Piece.White);
            squares[i, 6].SetPieceAndColor(Piece.Pawn, Piece.Black);
        }
        
        squares[0, 7].SetPieceAndColor(Piece.Rook, Piece.Black);
        squares[1, 7].SetPieceAndColor(Piece.Knight, Piece.Black);
        squares[2, 7].SetPieceAndColor(Piece.Bishop, Piece.Black);
        squares[3, 7].SetPieceAndColor(Piece.Queen, Piece.Black);
        squares[4, 7].SetPieceAndColor(Piece.King, Piece.Black);
        squares[5, 7].SetPieceAndColor(Piece.Bishop, Piece.Black);
        squares[6, 7].SetPieceAndColor(Piece.Knight, Piece.Black);
        squares[7, 7].SetPieceAndColor(Piece.Rook, Piece.Black);

        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                squares[file, rank].SetFile(file);
                squares[file, rank].SetRank(rank);
            }
        }
    }

    public Square[, ] GetSquares()
    {
        return squares;
    }

    public Coord[] GenerateMoves(Coord pieceCoord)
    {
        Coord[] emptyMoveList = new Coord[2];
        
        switch (squares[pieceCoord.fileIndex, pieceCoord.rankIndex].GetPiece()) {
            case Piece.Pawn:
                return emptyMoveList;
            case Piece.Knight:
                return GenerateKnightMoves(pieceCoord);
            case Piece.Bishop:
                return emptyMoveList;
            case Piece.Rook:
                return emptyMoveList;
            case Piece.Queen:
                return emptyMoveList;
            case Piece.King:
                return emptyMoveList;
            default:
                return emptyMoveList;
        }
    }

    private Coord IsCoordValid(int file, int rank, int pieceColor)
    {
        return (file < 8 && file > -1 && rank < 8 && rank > -1 && squares[file, rank].GetColor() != pieceColor) ? new Coord(file, rank) : new Coord(-1,-1);
    }

    public bool PawnToMove(Coord startCoord, Coord endCoord)
    {
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;

        return true;
    }

    public bool KnightToMove(Coord startCoord, Coord endCoord)
    {
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;

        bool moveIsLegal = false;
        Coord[] legalMoves = GenerateKnightMoves(startCoord);
        for (int i = 0; i < legalMoves.Length; i++) {
            if (legalMoves[i].fileIndex == endFile && legalMoves[i].rankIndex == endRank) {
                moveIsLegal = true;
            }
        }

        return moveIsLegal;
    }


    public Coord[] GenerateKnightMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[8];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();
        Coord empty = new Coord(-1,-1);
        int newFile;
        int newRank;
        
        // Jumping up
        newRank = pieceRank + 2;
        newFile = pieceFile + 1;
        moves[0] = IsCoordValid(newFile, newRank, pieceColor);
        newFile = pieceFile - 1;
        moves[1] = IsCoordValid(newFile, newRank, pieceColor);
        
        // Jumping down
        newRank = pieceRank - 2;
        moves[2] = IsCoordValid(newFile, newRank, pieceColor);
        newFile = pieceFile + 1;
        moves[3] = IsCoordValid(newFile, newRank, pieceColor);
        
        // Jumping right
        newFile = pieceFile + 2;
        newRank = pieceRank + 1;
        moves[4] = IsCoordValid(newFile, newRank, pieceColor);
        newRank = pieceRank - 1;
        moves[5] = IsCoordValid(newFile, newRank, pieceColor);
        
        // Jumping left
        newFile = pieceFile - 2;
        moves[6] = IsCoordValid(newFile, newRank, pieceColor);
        newRank = pieceRank + 1;
        moves[7] = IsCoordValid(newFile, newRank, pieceColor);

        return moves;
    }

    public bool BishopToMove(Coord startCoord, Coord endCoord)
    {
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;

        return true;
    }

    public bool RookToMove(Coord startCoord, Coord endCoord)
    {
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;

        return true;
    }

    public bool QueenToMove(Coord startCoord, Coord endCoord)
    {
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;

        return true;
    }

    public bool KingToMove(Coord startCoord, Coord endCoord)
    {
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;

        return true;
    }

    public bool IsMoveLegal(Coord startCoord, Coord endCoord)
    {
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int movingPiece = squares[startFile, startRank].GetPiece();
        
        switch (movingPiece) {
            case Piece.Pawn:
                return PawnToMove(startCoord, endCoord);
            case Piece.Knight:
                return KnightToMove(startCoord, endCoord);
            case Piece.Bishop:
                return BishopToMove(startCoord, endCoord);
            case Piece.Rook:
                return RookToMove(startCoord, endCoord);
            case Piece.Queen:
                return QueenToMove(startCoord, endCoord);
            case Piece.King:
                return KingToMove(startCoord, endCoord);
            default:
                return false;
        }
    }

    public void MakeMove(Coord startCoord, Coord endCoord)
    {
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;

        squares[endFile, endRank].SetPieceAndColor(squares[startFile, startRank].GetPiece(), squares[startFile, startRank].GetColor());
        squares[startFile, startRank].SetEmpty();
        if (colorToMove == Piece.White) {
            colorToMove = Piece.Black;
        } else {
            colorToMove = Piece.White;
        }
    }
}
