using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardUI : MonoBehaviour
{
    public BoardTheme boardTheme;
    public PieceTheme pieceTheme;
    public Board board = new Board();
    public bool whiteIsBottom = true;
    MeshRenderer[, ] squareRenderers;
    SpriteRenderer[, ] squarePieceRenderers;

    const float pieceDepth = -0.1f;
    const float pieceDragDepth = -0.2f;
    private Coord lastStartSquare;
    private Coord lastTargetSquare;
    public bool highlight = true;
    
    void Awake()
    {
        CreateBoardUI();
        //UpdatePosition(board);
    }

    public void DragPiece (Coord pieceCoord, Vector2 mousePos) {
        squarePieceRenderers[pieceCoord.fileIndex, pieceCoord.rankIndex].transform.position = new Vector3 (mousePos.x, mousePos.y, pieceDragDepth);
    }

    public void ResetPiecePosition (Coord pieceCoord) {
        Vector3 pos = PositionFromCoord (pieceCoord.fileIndex, pieceCoord.rankIndex, pieceDepth);
        squarePieceRenderers[pieceCoord.fileIndex, pieceCoord.rankIndex].transform.position = pos;
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
            if (moves[i].fileIndex < 8 && moves[i].fileIndex > -1 && moves[i].rankIndex < 8 && moves[i].rankIndex > -1) {
                SetSquareColor(moves[i], boardTheme.lightSquares.legal, boardTheme.darkSquares.legal);
            }
        } 
    }

    public void OnMoveMade(Board board, Coord startSquare, Coord targetSquare)
    {
        lastStartSquare = startSquare;
        lastTargetSquare = targetSquare;
        UpdatePosition(board);
        ResetSquareColors();
    }

    public bool TryGetSquareUnderMouse (Vector2 mouseWorld, out Coord selectedCoord) {
        int file = (int) (mouseWorld.x + 4);
        int rank = (int) (mouseWorld.y + 4);
        if (!whiteIsBottom) {
            file = 7 - file;
            rank = 7 - rank;
        }
        selectedCoord = new Coord (file, rank);
        return file >= 0 && file < 8 && rank >= 0 && rank < 8;
    }

    public void UpdatePosition(Board board) {
        Square[,] squares = board.GetSquares();
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                int pieceType = squares[file, rank].GetPiece();
                int pieceColor = squares[file, rank].GetColor();

                squarePieceRenderers[file, rank].sprite = pieceTheme.GetPieceSprite(pieceType, pieceColor);
                squarePieceRenderers[file, rank].transform.position = PositionFromCoord(file, rank);
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
        squareRenderers = new MeshRenderer[8, 8];
        squarePieceRenderers = new SpriteRenderer[8, 8];

        for (int rank = 0; rank < 8; rank++) {
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
                pieceRenderer.transform.localScale = Vector3.one * 100 / (12000 / 6f);
                squarePieceRenderers[file, rank] = pieceRenderer;
            }
        }
        ResetSquareColors();
    }

    public Vector3 PositionFromCoord (int file, int rank, float depth = 0)
    {
        return new Vector3 (-3.5f + file, -3.5f + rank, depth);
    }

    void SetSquareColor(Coord coord, Color lightColor, Color darkColor)
    {
        int file = coord.fileIndex;
        int rank = coord.rankIndex;
        squareRenderers[file, rank].material.color = (IsLightSquare(file, rank)) ? lightColor : darkColor;
    }

    public void ResetSquareColors()
    {
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                SetSquareColor(new Coord(file, rank), boardTheme.lightSquares.normal, boardTheme.darkSquares.normal);
            }
        }
        if (highlight) {
            if (lastStartSquare != null) {
                HighlightLastMove(lastStartSquare, lastTargetSquare);
            }
        }
    }
}
