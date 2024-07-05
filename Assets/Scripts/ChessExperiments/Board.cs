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

    private Coord IsCoordValid(int file, int rank, int pieceColor)
    {
        return (file < 8 && file > -1 && rank < 8 && rank > -1 && squares[file, rank].GetColor() != pieceColor) ? new Coord(file, rank) : new Coord(-1,-1);
    }

    public bool PieceToMove(Coord startCoord, Coord endCoord)
    {
        return true;
    }

    public Coord[] GeneratePawnMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[64];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();

        // This makes all squares legal moves to be a functioning stub.
        int moveIndex = 0;
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                if (IsCoordValid(file, rank, pieceColor).fileIndex != -1) {
                    moves[moveIndex] = new Coord(file, rank);
                } else {
                    moves[moveIndex] = new Coord(-1,-1);
                }
                moveIndex++;
            }
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

    public Coord[] GenerateBishopMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[64];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();

        // This makes all squares legal moves to be a functioning stub.
        int moveIndex = 0;
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                if (IsCoordValid(file, rank, pieceColor).fileIndex != -1) {
                    moves[moveIndex] = new Coord(file, rank);
                } else {
                    moves[moveIndex] = new Coord(-1,-1);
                }
                moveIndex++;
            }
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
                if (IsCoordValid(file, rank, pieceColor).fileIndex != -1) {
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
                if (IsCoordValid(file, rank, pieceColor).fileIndex != -1) {
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

        // This makes all squares legal moves to be a functioning stub.
        int moveIndex = 0;
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                if (IsCoordValid(file, rank, pieceColor).fileIndex != -1) {
                    moves[moveIndex] = new Coord(file, rank);
                } else {
                    moves[moveIndex] = new Coord(-1,-1);
                }
                moveIndex++;
            }
        }

        return moves;
    }

    public bool IsMoveLegal(Coord startCoord, Coord endCoord)
    {
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
