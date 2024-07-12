/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;*/

using System;
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
        // Set the board with the starting pieces in their starting positions
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
        /*
        Generate a list of coordinates that are legal moves for the piece at the given coordinate.
        Each unique generate moves function takes care of a single piece type. 
        */
        switch (squares[pieceCoord.fileIndex, pieceCoord.rankIndex].GetPiece()) {
            case Piece.Pawn:
                return GeneratePawnMoves(pieceCoord);
            case Piece.Knight:
                return GenerateKnightMoves(pieceCoord);
            case Piece.Bishop:
                return GenerateBishopMoves(pieceCoord);
            case Piece.Rook:
                return GenerateRookMoves(pieceCoord);
            case Piece.Queen:
                return GenerateQueenMoves(pieceCoord);
            case Piece.King:
                return GenerateKingMoves(pieceCoord);
            default:
                return new Coord[2];
        }
    }

    private Coord GiveValidCoord(int file, int rank, int pieceColor)
    {
        /*
        This is for checking a coordinate to make sure that it is on the board and
        is not the current piece's color. Otherwise it will return a coordinate that
        won't display or work. This is to make it easier to get coordinates for the
        move generators.
        */
        return (file < 8 && file > -1 && rank < 8 && rank > -1 && squares[file, rank].GetColor() != pieceColor) ? new Coord(file, rank) : new Coord(-1,-1);
    }

    public Coord[] GeneratePawnMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[64];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();
        int newRank;
        int newFile;
        
        // Pawn Single & Double
        // newRank = pieceRank + 2;
        // newFile = pieceFile + 0;
        // moves[0] = GiveValidCoord(newFile, newRank, pieceColor);
        // newRank = pieceRank - 2;
        // moves[1] = GiveValidCoord(newFile, newRank, pieceColor);
        // newRank = pieceRank + 1;
        // moves[2] = GiveValidCoord(newFile, newRank, pieceColor);
        // newRank = pieceRank - 1;
        // moves[3] = GiveValidCoord(newFile, newRank, pieceColor);
        
        if (pieceRank == 1 || pieceColor == Piece.White)
        {
            // Pawn Single & Double
            newRank = pieceRank + 2;
            newFile = pieceFile + 0;
            moves[0] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank = pieceRank - 2;
            moves[1] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank = pieceRank + 1;
            moves[2] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank = pieceRank - 1;
            moves[3] = GiveValidCoord(newFile, newRank, pieceColor);
        } else if (pieceRank == 6 || pieceColor == Piece.Black)
        {
            // Pawn Single & Double
            newRank = pieceRank + 2;
            newFile = pieceFile + 0;
            moves[0] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank = pieceRank - 2;
            moves[1] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank = pieceRank + 1;
            moves[2] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank = pieceRank - 1;
            moves[3] = GiveValidCoord(newFile, newRank, pieceColor);
        }
        else
        {
            // Pawn Single Move
            newRank = pieceRank + 1;
            newFile = pieceFile + 0;
            moves[4] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank = pieceRank - 1;
            moves[5] = GiveValidCoord(newFile, newRank, pieceColor);   
        }

        return moves;
    }


    public Coord[] GenerateKnightMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[8];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();
        int newFile;
        int newRank;
        
        // Jumping up
        newRank = pieceRank + 2;
        newFile = pieceFile + 1;
        moves[0] = GiveValidCoord(newFile, newRank, pieceColor);
        newFile = pieceFile - 1;
        moves[1] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Jumping down
        newRank = pieceRank - 2;
        moves[2] = GiveValidCoord(newFile, newRank, pieceColor);
        newFile = pieceFile + 1;
        moves[3] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Jumping right
        newFile = pieceFile + 2;
        newRank = pieceRank + 1;
        moves[4] = GiveValidCoord(newFile, newRank, pieceColor);
        newRank = pieceRank - 1;
        moves[5] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Jumping left
        newFile = pieceFile - 2;
        moves[6] = GiveValidCoord(newFile, newRank, pieceColor);
        newRank = pieceRank + 1;
        moves[7] = GiveValidCoord(newFile, newRank, pieceColor);

        return moves;
    }

    public Coord[] GenerateBishopMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[64];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();

        return GetDiagonalMoves(pieceCoord);
    }

    public Coord[] GetDiagonalMoves(Coord pieceCoord)
    {
        // Initialize all the attributes
        Coord[] moves = new Coord[64];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();
        int newFile;
        int newRank;
        int moveIndex = 0;
        bool canContinue = true;
        
        // Move Upper Right
        newRank = pieceRank + 1;
        newFile = pieceFile + 1;
        while (canContinue == true)
        {
            if (newFile > 7 || newFile < 0 || newRank > 7 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
            {
                canContinue = false;
            }
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank++;
            newFile++;
            moveIndex++;
        }

        canContinue = true;
        newRank = pieceRank + 1;
        newFile = pieceFile - 1;
        while (canContinue == true)
        {
            if (newFile > 7 || newFile < 0 || newRank > 7 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
            {
                canContinue = false;
            }
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank++;
            newFile--;
            moveIndex++;
        }
        
        canContinue = true;
        newRank = pieceRank - 1;
        newFile = pieceFile - 1;
        while (canContinue == true)
        {
            if (newFile > 7 || newFile < 0 || newRank > 7 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
            {
                canContinue = false;
            }
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank--;
            newFile--;
            moveIndex++;
        }
        
        canContinue = true;
        newRank = pieceRank - 1;
        newFile = pieceFile + 1;
        while (canContinue == true)
        {
            if (newFile > 7 || newFile < 0 || newRank > 7 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
            {
                canContinue = false;
            }
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank--;
            newFile++;
            moveIndex++;
        }
        
        return moves;
    }

    public Coord[] GenerateRookMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[64];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();

        // This makes all squares legal moves to be a functioning stub.
        int moveIndex = 0;
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                if (GiveValidCoord(file, rank, pieceColor).fileIndex != -1) {
                    moves[moveIndex] = new Coord(file, rank);
                } else {
                    moves[moveIndex] = new Coord(-1,-1);
                }
                moveIndex++;
            }
        }

        return moves;
    }

    public Coord[] GenerateQueenMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[64];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();

        // This makes all squares legal moves to be a functioning stub.
        int moveIndex = 0;
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                if (GiveValidCoord(file, rank, pieceColor).fileIndex != -1) {
                    moves[moveIndex] = new Coord(file, rank);
                } else {
                    moves[moveIndex] = new Coord(-1,-1);
                }
                moveIndex++;
            }
        }

        return moves;
    }

    public Coord[] GenerateKingMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[64];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();
        int newRank;
        int newFile;
        
        // Forward
        newRank = pieceRank + 1;
        newFile = pieceFile + 0;
        moves[0] = GiveValidCoord(newFile, newRank, pieceColor);
        newRank = pieceRank - 1;
        moves[1] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Backward
        newRank = pieceRank - 1;
        moves[2] = GiveValidCoord(newFile, newRank, pieceColor);
        newRank = pieceRank + 1;
        moves[3] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Right
        newFile = pieceFile + 1;
        moves[4] = GiveValidCoord(newFile, newRank, pieceColor);
        newFile = pieceFile - 1;
        moves[5] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Left
        newFile = pieceFile - 1;
        moves[6] = GiveValidCoord(newFile, newRank, pieceColor);
        newFile = pieceFile + 1;
        moves[7] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Upper Right
        newFile = pieceFile + 1;
        newRank = pieceRank + 1;
        moves[8] = GiveValidCoord(newFile, newRank, pieceColor);
        newFile = pieceFile - 1;
        newRank = pieceRank - 1;
        moves[9] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Upper Left
        newFile = pieceFile - 1;
        newRank = pieceRank + 1;
        moves[10] = GiveValidCoord(newFile, newRank, pieceColor);
        newFile = pieceFile + 1;
        newRank = pieceRank - 1;
        moves[11] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Lower Right
        newFile = pieceFile + 1;
        newRank = pieceRank - 1;
        moves[12] = GiveValidCoord(newFile, newRank, pieceColor);
        newFile = pieceFile - 1;
        newRank = pieceRank + 1;
        moves[13] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Lower Left
        newFile = pieceFile - 1;
        newRank = pieceRank - 1;
        moves[14] = GiveValidCoord(newFile, newRank, pieceColor);
        newFile = pieceFile + 1;
        newRank = pieceRank + 1;
        moves[15] = GiveValidCoord(newFile, newRank, pieceColor);

        return moves;
    }

    public bool IsMoveLegal(Coord startCoord, Coord endCoord)
    {
        /*
        Check if 
        */
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;
        int movingPiece = squares[startFile, startRank].GetPiece();
        bool moveIsLegal = false;
        Coord[] legalMoves;

        switch (movingPiece) {
            case Piece.Pawn:
                legalMoves = GeneratePawnMoves(startCoord);
                break;
            case Piece.Knight:
                legalMoves = GenerateKnightMoves(startCoord);
                break;
            case Piece.Bishop:
                legalMoves = GenerateBishopMoves(startCoord);
                break;
            case Piece.Rook:
                legalMoves = GenerateRookMoves(startCoord);
                break;
            case Piece.Queen:
                legalMoves = GenerateQueenMoves(startCoord);
                break;
            case Piece.King:
                legalMoves = GenerateKingMoves(startCoord);
                break;
            default:
                legalMoves = new Coord[2];
                break;
        }

        for (int i = 0; i < legalMoves.Length; i++) {
            try {
                if (legalMoves[i].fileIndex == endFile && legalMoves[i].rankIndex == endRank) {
                    moveIsLegal = true;
                }
            }
            catch (NullReferenceException)
            {
                break;
            }

        }

        return moveIsLegal;
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
