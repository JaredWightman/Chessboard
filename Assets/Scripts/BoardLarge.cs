using System;
using System.IO;

public class BoardLarge
{
	public Square[,] squares;
	public const int width = 14;
	public const int height = 8;
	public bool isGameOver = false;
    public bool blackWon = false;
    public bool whiteWon = false;

	public int colorToMove = Piece.White;

	public BoardLarge()
	{
		SetBoard();
	}

	public void SetBoard()
	{
		Square s = new Square();
		
		// Set the board with the starting pieces in their starting positions
		squares = new Square[width, height];

		for (int file = 0; file < width; file++) {
			for (int rank = 0; rank < height; rank++) {
				squares[file, rank] = new Square();
			}
		}

		// Set up white pieces
		squares[0, 0].SetPieceAndColor(Piece.Rook, Piece.White);
		squares[1, 0].SetPieceAndColor(Piece.Knight, Piece.White);
		squares[2, 0].SetPieceAndColor(Piece.Bishop, Piece.White);
		squares[3, 0].SetPieceAndColor(Piece.Rook, Piece.White);
		squares[4, 0].SetPieceAndColor(Piece.Bishop, Piece.White);
		squares[5, 0].SetPieceAndColor(Piece.King, Piece.White);
		squares[6, 0].SetPieceAndColor(Piece.Queen, Piece.White);
		squares[7, 0].SetPieceAndColor(Piece.Bishop, Piece.White);
		squares[8, 0].SetPieceAndColor(Piece.King, Piece.White);
		squares[9, 0].SetPieceAndColor(Piece.Queen, Piece.White);
		squares[10, 0].SetPieceAndColor(Piece.Rook, Piece.White);
		squares[11, 0].SetPieceAndColor(Piece.Bishop, Piece.White);
		squares[12, 0].SetPieceAndColor(Piece.Knight, Piece.White);
		squares[13, 0].SetPieceAndColor(Piece.Rook, Piece.White);
		squares[0, 1].SetPieceAndColor(Piece.Knight, Piece.White);
		squares[13, 1].SetPieceAndColor(Piece.Knight, Piece.White);

		// Set all pawns
		for (int i = 1; i < 13; i++)
		{
			squares[i, 1].SetPieceAndColor(Piece.Pawn, Piece.White);
			squares[i, 6].SetPieceAndColor(Piece.Pawn, Piece.Black);
		}

		// Set up black pieces
		squares[0,7].SetPieceAndColor(Piece.Rook, Piece.Black);
		squares[1, 7].SetPieceAndColor(Piece.Knight, Piece.Black);
		squares[2, 7].SetPieceAndColor(Piece.Bishop, Piece.Black);
		squares[3, 7].SetPieceAndColor(Piece.Rook, Piece.Black);
		squares[4, 7].SetPieceAndColor(Piece.Bishop, Piece.Black);
		squares[5, 7].SetPieceAndColor(Piece.King, Piece.Black);
		squares[6, 7].SetPieceAndColor(Piece.Queen, Piece.Black);
		squares[7, 7].SetPieceAndColor(Piece.Bishop, Piece.Black);
		squares[8, 7].SetPieceAndColor(Piece.King, Piece.Black);
		squares[9, 7].SetPieceAndColor(Piece.Queen, Piece.Black);
		squares[10, 7].SetPieceAndColor(Piece.Rook, Piece.Black);
		squares[11, 7].SetPieceAndColor(Piece.Bishop, Piece.Black);
		squares[12, 7].SetPieceAndColor(Piece.Knight, Piece.Black);
		squares[13, 7].SetPieceAndColor(Piece.Rook, Piece.Black);
		squares[0, 6].SetPieceAndColor(Piece.Knight, Piece.Black);
		squares[13, 6].SetPieceAndColor(Piece.Knight, Piece.Black);

		// Init each square's rank and file values
		for (int rank = 0; rank < height; rank++)
		{
			for (int file = 0; file < width; file++)
			{
				squares[file, rank].SetFile(file);
				squares[file, rank].SetRank(rank);
			}
		}
	}

	public Square[,] GetSquares()
	{
		return squares;
	}

	public Coord[] GenerateMoves(Coord pieceCoord)
	{
		/*
        Generate a list of coordinates that are legal moves for the piece at the given coordinate.
        Each unique generate moves function takes care of a single piece type. 
        */
		switch (squares[pieceCoord.fileIndex, pieceCoord.rankIndex].GetPiece())
		{
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
		return (file < width && file > -1 && rank < height && rank > -1 && squares[file, rank].GetColor() != pieceColor) ? new Coord(file, rank) : new Coord(-1, -1);// (-1,-1) takes up space in arrays and isn't necessary (see rook). Instead return null.
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
			if (squares[pieceFile, newRank].GetColor() == Piece.None)
			{

				moves[moveIndex] = GiveValidCoord(pieceFile, newRank, pieceColor);
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

			// Pawn capture
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
		}

		if (squares[pieceFile, pieceRank].GetColor() == Piece.Black)
		{
			newRank = pieceRank - 1;
			newFile = pieceFile + 0;
			if (squares[pieceFile, newRank].GetColor() == Piece.None)
			{
				moves[moveIndex] = GiveValidCoord(pieceFile, newRank, pieceColor);
				if (pieceRank == height - 2)
				{
					if (squares[pieceFile, pieceRank - 2].GetColor() == Piece.None)
					{
						moveIndex++;
						moves[moveIndex] = GiveValidCoord(pieceFile, pieceRank - 2, pieceColor);
					}
				}

				moveIndex++;
			}

			// Pawn Capture
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
                moveIndex++;
            }
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
			if (newFile > width - 1 || newFile < 0 || newRank > height - 1 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
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
			if (newFile > width - 1 || newFile < 0 || newRank > height - 1 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
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
			if (newFile > width - 1 || newFile < 0 || newRank > height - 1 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
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
			if (newFile > width - 1 || newFile < 0 || newRank > height - 1 || newRank < 0 || squares[newFile, newRank].GetPiece() != Piece.None)
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
		Coord[] moves = new Coord[22];
		int pieceFile = pieceCoord.fileIndex;
		int pieceRank = pieceCoord.rankIndex;
		int pieceColor = squares[pieceFile, pieceRank].GetColor();
		int newFile;
		int newRank;
		int moveIndex = 0;

		// Up
		newRank = pieceRank + 1;
		newFile = pieceFile;

		while (newRank <= height - 1 && GiveValidCoord(newFile, newRank, pieceColor) != null && (squares[newFile, newRank - 1].GetPiece() == Piece.None || newRank - 1 == pieceRank))
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
		Coord[] moves = new Coord[14];
		int pieceFile = pieceCoord.fileIndex;
		int pieceRank = pieceCoord.rankIndex;
		int pieceColor = squares[pieceFile, pieceRank].GetColor();
		int newRank;
		int newFile;
		int moveIndex = 0;

		// Forward
		newRank = pieceRank + 1;
		newFile = pieceFile;
		if (GiveValidCoord(newFile, newRank, pieceColor) != null)
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Backward
		newRank = pieceRank - 1;
		newFile = pieceFile;
		if (GiveValidCoord(newFile, newRank, pieceColor) != null)
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Right
		newFile = pieceFile + 1;
		newRank = pieceRank;
		if (GiveValidCoord(newFile, newRank, pieceColor) != null)
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Left
		newFile = pieceFile - 1;
		newRank = pieceRank;
		if (GiveValidCoord(newFile, newRank, pieceColor) != null)
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Upper Right
		newFile = pieceFile + 1;
		newRank = pieceRank + 1;
		if (GiveValidCoord(newFile, newRank, pieceColor) != null)
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Upper Left
		newFile = pieceFile - 1;
		newRank = pieceRank + 1;
		if (GiveValidCoord(newFile, newRank, pieceColor) != null)
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Lower Right
		newFile = pieceFile + 1;
		newRank = pieceRank - 1;
		if (GiveValidCoord(newFile, newRank, pieceColor) != null)
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Lower Left
		newFile = pieceFile - 1;
		newRank = pieceRank - 1;
		if (GiveValidCoord(newFile, newRank, pieceColor) != null)
		{
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Castle Code (may require move index)
		newFile = pieceFile;
		newRank = pieceRank;
		if (pieceFile == 8 && (pieceRank == 0 || pieceRank == 7) && squares[newFile + 1, newRank].GetPiece() == Piece.None && squares[newFile + 2, newRank].GetPiece() == Piece.None && squares[newFile + 3, newRank].GetPiece() == Piece.None && squares[newFile + 4, newRank].GetPiece() == Piece.None && squares[pieceFile + 5, pieceRank].GetPiece() == Piece.Rook && squares[pieceFile + 5, pieceRank].GetColor() == pieceColor)
		{
			// Castle right side (from white)
			newFile = pieceFile + 3;
			newRank = pieceRank;
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
			moveIndex++;
		}

		// Castle left side (from white)
		if (pieceFile == 5 && (pieceRank == 0 || pieceRank == 7) && squares[newFile - 1, newRank].GetPiece() == Piece.None && squares[newFile - 2, newRank].GetPiece() == Piece.None && squares[newFile - 3, newRank].GetPiece() == Piece.None && squares[newFile - 4, newRank].GetPiece() == Piece.None && squares[pieceFile - 5, pieceRank].GetPiece() == Piece.Rook && squares[pieceFile - 5, pieceRank].GetColor() == pieceColor)
		{
			// Castle right side (from white)
			newFile = pieceFile - 3;
			newRank = pieceRank;
			moves[moveIndex] = GiveValidCoord(newFile, newRank, pieceColor);
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

		switch (movingPiece)
		{
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

		for (int i = 0; i < legalMoves.Length; i++)
		{
			try
			{
				if (legalMoves[i].fileIndex == endFile && legalMoves[i].rankIndex == endRank)
				{
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

		// King capture
        if (squares[endFile, endRank].GetPiece() == Piece.King)
        {
            isGameOver = true;
			if (squares[endFile, endRank].GetColor() == Piece.White)
            {
                blackWon = true;
            } else if (squares[endFile, endRank].GetColor() == Piece.Black)
            {
				whiteWon = true;
			}
		}

		// Pawn promotion
		if (squares[startFile, startRank].GetPiece() == Piece.Pawn)
		{
			if (squares[startFile, startRank].GetColor() == Piece.White)
			{
				if (endRank == height - 1)
				{
					pieceType = Piece.Queen;
				}
			}
			else if (squares[startFile, startRank].GetColor() == Piece.Black)
			{
				if (endRank == 0)
				{
					pieceType = Piece.Queen;
				}
			}
		}

		// Castle king
		if (squares[startFile, startRank].GetPiece() == Piece.King)
		{
			if (endFile == startFile + 3 && endRank == startRank)
			{
				squares[endFile - 1, endRank].SetPieceAndColor(Piece.Rook, squares[startFile, startRank].GetColor());
				squares[endFile + 2, endRank].SetEmpty();
			}
			else if (endFile == startFile - 3 && endRank == startRank)
			{
				squares[endFile + 1, endRank].SetPieceAndColor(Piece.Rook, squares[startFile, startRank].GetColor());
				squares[endFile - 2, endRank].SetEmpty();
			}
		}

		squares[endFile, endRank].SetPieceAndColor(pieceType, squares[startFile, startRank].GetColor());
		squares[startFile, startRank].SetEmpty();

		if (isGameOver)
        {
            colorToMove = 100;
        } else if (colorToMove == Piece.White) {
            colorToMove = Piece.Black;
        } else {
            colorToMove = Piece.White;
        }
	}
}
