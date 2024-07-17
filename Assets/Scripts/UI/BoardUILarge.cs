using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoardUILarge : MonoBehaviour
{
    public BoardTheme boardTheme;
    public PieceTheme pieceTheme;
    // cburnett is 100 / (12000 / 6f) or 1 / 20f which is 0.05
    // merida size: 100 / (1300 / 6f) or 6 / 13f which is 0.145131429, pieces link https://www.zoomchess.com/images/sets/merida/
    public float pieceSizeModifier = 100 / (12000 / 6f);
    public bool doFlipBoard = true;
    public BoardLarge board = new BoardLarge();
    public bool whiteIsBottom = true;
    MeshRenderer[, ] squareRenderers;
    SpriteRenderer[, ] squarePieceRenderers;
	// public Text blackWins;
	// public Text whiteWins;

	const float pieceDepth = -0.1f;
    const float pieceDragDepth = -0.2f;
    private Coord lastStartSquare;
    private Coord lastTargetSquare;
    public bool highlight = true;
    
    void Awake()
    {
        CreateBoardUI();
		// whiteWins.enabled = false;
		// blackWins.enabled = false;
		//UpdatePosition(board);
	}

    public void DragPiece (Coord pieceCoord, Vector2 mousePos) {
        if (whiteIsBottom) {
            squarePieceRenderers[pieceCoord.fileIndex, pieceCoord.rankIndex].transform.position = new Vector3 (mousePos.x, mousePos.y, pieceDragDepth);
        } else {
            squarePieceRenderers[13 - pieceCoord.fileIndex, 7 - pieceCoord.rankIndex].transform.position = new Vector3 (mousePos.x, mousePos.y, pieceDragDepth);

        }
    }

    public void ResetPiecePosition (Coord pieceCoord) {
        if (whiteIsBottom) {
            Vector3 pos = PositionFromCoord (pieceCoord.fileIndex, pieceCoord.rankIndex, pieceDepth);
            squarePieceRenderers[pieceCoord.fileIndex, pieceCoord.rankIndex].transform.position = pos;
        } else {
            Vector3 pos = PositionFromCoord (13 - pieceCoord.fileIndex, 7 - pieceCoord.rankIndex, pieceDepth);
            squarePieceRenderers[13 - pieceCoord.fileIndex, 7 - pieceCoord.rankIndex].transform.position = pos;
        }
    }

    public void SelectSquare (Coord coord) {
        SetSquareColor(coord, boardTheme.lightSquares.selected, boardTheme.darkSquares.selected);
    }

    public void DeselectSquare (Coord coord) {
        ResetSquareColors ();
    }

    void HighlightLastMove(Coord startCoord, Coord targetCoord)
    {
        SetSquareColor(startCoord, boardTheme.lightSquares.moveFromHighlight, boardTheme.darkSquares.moveFromHighlight);
        SetSquareColor(targetCoord, boardTheme.lightSquares.moveToHighlight, boardTheme.darkSquares.moveToHighlight);
    }

    public void HighlightLegalMoves(Coord[] moves)
    {
        for (int i = 0; i < moves.Length; i++) {
            if (moves[i].fileIndex < 14 && moves[i].fileIndex > -1 && moves[i].rankIndex < 8 && moves[i].rankIndex > -1) {
                SetSquareColor(moves[i], boardTheme.lightSquares.legal, boardTheme.darkSquares.legal);
            }
        } 
    }

    public void OnMoveMade(BoardLarge board, Coord startSquare, Coord targetSquare)
    {
        lastStartSquare = startSquare;
        lastTargetSquare = targetSquare;
        if (doFlipBoard) {
            FlipBoard();
        }
        UpdatePosition(board);
        ResetSquareColors();
		// if (board.blackWon)
		// {
		// 	blackWins.enabled = true;
		// }
		// else if (board.whiteWon)
		// {
		// 	whiteWins.enabled = true;
		// }
	}

    public bool TryGetSquareUnderMouse (Vector2 mouseWorld, out Coord selectedCoord) {
		//int file = (int) (mouseWorld.x + 4);
		int file = (int)(mouseWorld.x + 7);
		int rank = (int) (mouseWorld.y + 4);
        if (whiteIsBottom == false) {
            file = 13 - file;
            rank = 7 - rank;
        }
        selectedCoord = new Coord (file, rank);
        return file >= 0 && file < 14 && rank >= 0 && rank < 8;
    }

    public void UpdatePosition(BoardLarge board) {
        Square[,] squares = board.GetSquares();
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 14; file++) {
                int pieceType = squares[file, rank].GetPiece();
                int pieceColor = squares[file, rank].GetColor();

                if (whiteIsBottom) {
                    squarePieceRenderers[file, rank].sprite = pieceTheme.GetPieceSprite(pieceType, pieceColor);
                    squarePieceRenderers[file, rank].transform.position = PositionFromCoord(file, rank);
                } else {
                    squarePieceRenderers[13 - file, 7 - rank].sprite = pieceTheme.GetPieceSprite(pieceType, pieceColor);
                    squarePieceRenderers[13 - file, 7 - rank].transform.position = PositionFromCoord(13 - file, 7 - rank);
                }
            }
        }
    }

    bool IsLightSquare(int file, int rank)
    {
        return ((file + rank) % 2 != 0);
    }

    void CreateBoardUI()
    {
        Shader squareShader = Shader.Find ("Unlit/Color");
        squareRenderers = new MeshRenderer[14, 14];
        squarePieceRenderers = new SpriteRenderer[14, 14];

        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 14; file++) {
                Transform square = GameObject.CreatePrimitive (PrimitiveType.Quad).transform;
                square.parent = transform;
                square.position = PositionFromCoord (file, rank, 0);
                Material squareMatierial = new Material (squareShader);

                squareRenderers[file, rank] = square.gameObject.GetComponent<MeshRenderer> ();
                squareRenderers[file, rank].material = squareMatierial;

                SpriteRenderer pieceRenderer = new GameObject ("Piece").AddComponent<SpriteRenderer> ();
                pieceRenderer.transform.parent = square;
                pieceRenderer.transform.position = PositionFromCoord (file, rank, pieceDepth);
                pieceRenderer.transform.localScale = Vector3.one * pieceSizeModifier;
                squarePieceRenderers[file, rank] = pieceRenderer;
            }
        }
        ResetSquareColors();
    }

    public Vector3 PositionFromCoord (int file, int rank, float depth = 0)
    {
        return new Vector3 (-6.5f + file, -3.5f + rank, depth);
    }

    void SetSquareColor(Coord coord, Color lightColor, Color darkColor)
    {
        if (whiteIsBottom == false) {
            coord = BlackOnBottomCoord(coord);
        }
        int file = coord.fileIndex;
        int rank = coord.rankIndex;
        squareRenderers[file, rank].material.color = (IsLightSquare(file, rank)) ? lightColor : darkColor;
    }

    public void ResetSquareColors()
    {
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 14; file++) {
                SetSquareColor(new Coord(file, rank), boardTheme.lightSquares.normal, boardTheme.darkSquares.normal);
            }
        }
        if (highlight) {
            if (lastStartSquare != null) {
                HighlightLastMove(lastStartSquare, lastTargetSquare);
            }
        }
    }

    private Coord BlackOnBottomCoord(Coord coord)
    {
        int newFile = 13 - coord.fileIndex;
        int newRank = 7 - coord.rankIndex;
        return new Coord(newFile, newRank);
    }

    public void FlipBoard()
    {
        if (whiteIsBottom) {
            whiteIsBottom = false;
        } else {
            whiteIsBottom = true;
        }
    }
}
