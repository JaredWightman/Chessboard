/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class Board
{
    public Square[, ] squares;
    public const int width = 8;
    public const int length = 8;

    public int colorToMove = Piece.White;

    public Board()
    {
        SetBoard();
    }

    public void SetBoard()
    {
        // Set the board with the starting pieces in their starting positions
        squares = new Square[length, width] {
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()},
            {new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square(), new Square()}
        };

        // Set up white pieces
        squares[0, 0].SetPieceAndColor(Piece.Rook, Piece.White);
        squares[1, 0].SetPieceAndColor(Piece.Knight, Piece.White);
        squares[2, 0].SetPieceAndColor(Piece.Bishop, Piece.White);
        squares[3, 0].SetPieceAndColor(Piece.Queen, Piece.White);
        squares[4, 0].SetPieceAndColor(Piece.King, Piece.White);
        squares[5, 0].SetPieceAndColor(Piece.Bishop, Piece.White);
        squares[6, 0].SetPieceAndColor(Piece.Knight, Piece.White);
        squares[7, 0].SetPieceAndColor(Piece.Rook, Piece.White);

        // Set all pawns
        for (int i = 0; i < 8; i++) {
            squares[i, 1].SetPieceAndColor(Piece.Pawn, Piece.White);
            squares[i, 6].SetPieceAndColor(Piece.Pawn, Piece.Black);
        }
        
        // Set up black pieces
        squares[0, 7].SetPieceAndColor(Piece.Rook, Piece.Black);
        squares[1, 7].SetPieceAndColor(Piece.Knight, Piece.Black);
        squares[2, 7].SetPieceAndColor(Piece.Bishop, Piece.Black);
        squares[3, 7].SetPieceAndColor(Piece.Queen, Piece.Black);
        squares[4, 7].SetPieceAndColor(Piece.King, Piece.Black);
        squares[5, 7].SetPieceAndColor(Piece.Bishop, Piece.Black);
        squares[6, 7].SetPieceAndColor(Piece.Knight, Piece.Black);
        squares[7, 7].SetPieceAndColor(Piece.Rook, Piece.Black);

        // Init each square's rank and file values
        for (int rank = 0; rank < length; rank++) {
            for (int file = 0; file < width; file++) {
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
        This is for checking a coordinate for a possible move to make sure that it is
        on the board and is not the color of the current piece trying to move to that
        sqaure. Otherwise it will return a coordinate that won't display or work. This
        is to make it easier to get coordinates for the move generators.
        */
        return (file < width && file > -1 && rank < length && rank > -1 && squares[file, rank].GetColor() != pieceColor) ? new Coord(file, rank) : new Coord(-1,-1);// (-1,-1) takes up space in arrays and isn't necessary (see rook). Instead return null.
    }

    public Coord[] GeneratePawnMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[6];
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();
        int newRank;
        int newFile;
        int moveIndex = 0;

        if (squares[pieceFile, pieceRank].GetColor() == Piece.White)
        {
            newRank = pieceRank + 1;
            newFile = pieceFile + 0;
            if (squares[pieceFile, newRank].GetColor() == Piece.None)
            {

                moves[moveIndex] = GiveValidCoord(pieceFile, newRank , pieceColor);
                if (pieceRank == 1)
                {
                    if (squares[pieceFile, pieceRank + 2].GetColor() == Piece.None)
                    {
                        moveIndex++;
                        newRank = pieceRank + 2;
                        moves[moveIndex] = GiveValidCoord(pieceFile, newRank, pieceColor);

                    }
                }

                moveIndex++;
            }
        }

        if (squares[pieceFile, pieceRank].GetColor() == Piece.Black)
        {
            newRank = pieceRank - 1;
            newFile = pieceFile + 0;
            if (squares[pieceFile, newRank].GetColor() == Piece.None)
            {
                moves[moveIndex] = GiveValidCoord(pieceFile, newRank, pieceColor);
                if (pieceRank == length - 2)
                {
                    if (squares[pieceFile, newRank -= 1].GetColor() == Piece.None)
                    {
                        moveIndex++;
                        moves[moveIndex] = GiveValidCoord(pieceFile, newRank, pieceColor);
                    }
                }

                moveIndex++;
            }
        }
        
        // Pawn Capture Code
        newFile = pieceFile - 1;
        newRank = pieceRank + 1;
        if (newFile > -1 && squares[newFile, newRank].GetColor() == Piece.Black)
        {
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
            moveIndex++;
        }
        
        newFile = pieceFile + 1;
        newRank = pieceRank + 1;
        if (newFile < width && squares[newFile, newRank].GetColor() == Piece.Black)
        {
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
            moveIndex++;
        }
        
        newFile = pieceFile - 1;
        newRank = pieceRank - 1;
        if (newFile > -1 && squares[newFile, newRank].GetColor() == Piece.White)
        {
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
            moveIndex++;
        }
        
        newFile = pieceFile + 1;
        newRank = pieceRank - 1;
        if (newFile < width && squares[newFile, newRank].GetColor() == Piece.White)
        {
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
        }


		Coord[] shmooves = new Coord[moveIndex];
		moveIndex = 0;
		foreach (Coord i in shmooves)
		{
			shmooves[moveIndex] = moves[moveIndex];
			moveIndex++;
		}
		return shmooves;
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
        // To the right
        newFile = pieceFile + 1;
        moves[0] = GiveValidCoord(newFile, newRank, pieceColor);
        // To the left
        newFile = pieceFile - 1;
        moves[1] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Jumping down
        newRank = pieceRank - 2;
        // Still to the left
        moves[2] = GiveValidCoord(newFile, newRank, pieceColor);
        // To the right
        newFile = pieceFile + 1;
        moves[3] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Jumping right
        newFile = pieceFile + 2;
        // Up
        newRank = pieceRank + 1;
        moves[4] = GiveValidCoord(newFile, newRank, pieceColor);
        // Down
        newRank = pieceRank - 1;
        moves[5] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Jumping left
        newFile = pieceFile - 2;
        // Still down
        moves[6] = GiveValidCoord(newFile, newRank, pieceColor);
        // Up
        newRank = pieceRank + 1;
        moves[7] = GiveValidCoord(newFile, newRank, pieceColor);

        return moves;
    }

    public Coord[] GenerateBishopMoves(Coord pieceCoord)
    {
        return GetDiagonalMoves(pieceCoord);
    }

    public Coord[] GetDiagonalMoves(Coord pieceCoord)
    {
        // Initialize all the attributes
        Coord[] moves = new Coord[18]; // Set to 14 once (-1, -1) isn't necessary (to limit loops and space)
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
            if (newFile > width - 1 || newFile < 0 || newRank > length - 1 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
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
            if (newFile > width - 1 || newFile < 0 || newRank > length - 1 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
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
            if (newFile > width - 1 || newFile < 0 || newRank > length - 1 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
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
            if (newFile > width - 1 || newFile < 0 || newRank > length - 1 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
            {
                canContinue = false;
            }
            moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
            newRank--;
            newFile++;
            moveIndex++;
        }

		Coord[] shmooves = new Coord[moveIndex];
		moveIndex = 0;
		foreach (Coord i in shmooves)
		{
			shmooves[moveIndex] = moves[moveIndex];
			moveIndex++;
		}
		return shmooves;
	}

	public Coord[] GetLinearMoves(Coord pieceCoord)
	{
		// Initialize all the attributes
		Coord[] moves = new Coord[14];
		int pieceFile = pieceCoord.fileIndex;
		int pieceRank = pieceCoord.rankIndex;
		int pieceColor = squares[pieceFile, pieceRank].GetColor();
		int newFile;
		int newRank;
		int moveIndex = 0;

		// Up
		newRank = pieceRank + 1;
		newFile = pieceFile;

		while (newRank <= length - 1 && GiveValidCoord(newFile, newRank, pieceColor) != null && (squares[newFile, newRank - 1].GetPiece() == Piece.None || newRank - 1 == pieceRank))
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
			newRank++;
		}

		// Down
		newRank = pieceRank - 1;
		newFile = pieceFile;

		while (newRank >= 0 && GiveValidCoord(newFile, newRank, pieceColor) != null && (squares[newFile, newRank + 1].GetPiece() == Piece.None || newRank + 1 == pieceRank))
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
			newRank--;
		}

		// Left
		newRank = pieceRank;
		newFile = pieceFile - 1;

		while (newFile >= 0 && GiveValidCoord(newFile, newRank, pieceColor) != null && (squares[newFile + 1, newRank].GetPiece() == Piece.None || newFile + 1 == pieceFile))
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
			newFile--;
		}

		// Right
		newRank = pieceRank;
		newFile = pieceFile + 1;

		while (newFile <= width - 1 && GiveValidCoord(newFile, newRank, pieceColor) != null && (squares[newFile - 1, newRank].GetPiece() == Piece.None || newFile - 1 == pieceFile))
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
			newFile++;
		}

		Coord[] shmooves = new Coord[moveIndex];
		moveIndex = 0;
		foreach (Coord i in shmooves)
		{
			shmooves[moveIndex] = moves[moveIndex];
			moveIndex++;
		}
		return shmooves;
	}


	public Coord[] GenerateRookMoves(Coord pieceCoord)
    {
		return GetLinearMoves(pieceCoord);
    }

    public Coord[] GenerateQueenMoves(Coord pieceCoord)
    {
        int moveIndex = 0;
        Coord[] moves = GetLinearMoves(pieceCoord);
        Coord[] morves = GetDiagonalMoves(pieceCoord);

		Coord[] shmooves = new Coord[moves.Length + morves.Length];
		foreach (Coord i in moves)
		{
			shmooves[moveIndex] = i;
			moveIndex++;
		}
		foreach (Coord i in morves)
		{
			shmooves[moveIndex] = i;
			moveIndex++;
		}
		return shmooves;
	}

    public Coord[] GenerateKingMoves(Coord pieceCoord)
    {
        Coord[] moves = new Coord[10]; // Increase and add iterator with castling moves
        int pieceFile = pieceCoord.fileIndex;
        int pieceRank = pieceCoord.rankIndex;
        int pieceColor = squares[pieceFile, pieceRank].GetColor();
        int newRank;
        int newFile;

		// Forward
		newRank = pieceRank + 1;
        newFile = pieceFile;
        moves[0] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Backward
        newRank = pieceRank - 1;
        newFile = pieceFile;
        moves[1] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Right
        newFile = pieceFile + 1;
        newRank = pieceRank;
        moves[2] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Left
        newFile = pieceFile - 1;
        newRank = pieceRank;
        moves[3] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Upper Right
        newFile = pieceFile + 1;
        newRank = pieceRank + 1;
        moves[4] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Upper Left
        newFile = pieceFile - 1;
        newRank = pieceRank + 1;
        moves[5] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Lower Right
        newFile = pieceFile + 1;
        newRank = pieceRank - 1;
        moves[6] = GiveValidCoord(newFile, newRank, pieceColor);
        
        // Lower Left
        newFile = pieceFile - 1;
        newRank = pieceRank - 1;
        moves[7] = GiveValidCoord(newFile, newRank, pieceColor);

		// Castle Code (may require move index)
        if (pieceFile == 4) {
            // Castle king side
            newFile = pieceFile + 2;
            newRank = pieceRank;
            if (squares[newFile, newRank].GetPiece() == Piece.None && squares[pieceFile + 1, pieceRank].GetPiece() == Piece.None && squares[pieceFile + 3, pieceRank].GetPiece() == Piece.Rook && squares[pieceFile + 3, pieceRank].GetColor() == pieceColor) {
                moves[8] = GiveValidCoord(newFile, newRank, pieceColor);
            }

            // Castle queen side
            newFile = pieceFile - 2;
            newRank = pieceRank;
            if (squares[newFile, newRank].GetPiece() == Piece.None && squares[pieceFile - 1, pieceRank].GetPiece() == Piece.None && squares[pieceFile - 3, pieceRank].GetPiece() == Piece.None && squares[pieceFile - 4, pieceRank].GetPiece() == Piece.Rook && squares[pieceFile - 4, pieceRank].GetColor() == pieceColor) {
                moves[9] = GiveValidCoord(newFile, newRank, pieceColor);
            }
        }

		return moves;
	}

    public bool IsMoveLegal(Coord startCoord, Coord endCoord)
    {
        /*
        Check if the coord to be moved to is in the list of legal moves for that piece type.
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
        /*
        Make the move from the start coordinate to the end coordinate and change turns.
        */
        int startFile = startCoord.fileIndex;
        int startRank = startCoord.rankIndex;
        int endFile = endCoord.fileIndex;
        int endRank = endCoord.rankIndex;
        int pieceType = squares[startFile, startRank].GetPiece();

        // Pawn promotion
        if (squares[startFile, startRank].GetPiece() == Piece.Pawn) {
            if (squares[startFile, startRank].GetColor() == Piece.White) {
                if (endRank == length - 1) {
                    pieceType = Piece.Queen;
                }
            } else if (squares[startFile, startRank].GetColor() == Piece.Black) {
                if (endRank == 0) {
                    pieceType = Piece.Queen;
                }
            }
        }

        // Castle king
        if (squares[startFile, startRank].GetPiece() == Piece.King) {
            if (endFile == startFile + 2 && endRank == startRank) {
                squares[endFile - 1, endRank].SetPieceAndColor(Piece.Rook, squares[startFile, startRank].GetColor());
                squares[endFile + 1, endRank].SetEmpty();
            } else if (endFile == startFile - 2 && endRank == startRank) {
                squares[endFile + 1, endRank].SetPieceAndColor(Piece.Rook, squares[startFile, startRank].GetColor());
                squares[endFile - 2, endRank].SetEmpty();
            }
        }

        squares[endFile, endRank].SetPieceAndColor(pieceType, squares[startFile, startRank].GetColor());
        squares[startFile, startRank].SetEmpty();
        if (colorToMove == Piece.White) {
            colorToMove = Piece.Black;
        } else {
            colorToMove = Piece.White;
        }
    }
}
