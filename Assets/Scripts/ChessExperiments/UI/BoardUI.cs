using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    void Awake()
    {
        CreateBoardUI();
        UpdatePosition(board);
    }

    public void UpdatePosition(Board board) {
        Square[,] squares = board.GetSquares();
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                int pieceType = squares[file, rank].getPiece();
                int pieceColor = squares[file, rank].getColor();

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

    void SetSquareColor(int file, int rank, Color lightColor, Color darkColor)
    {
        squareRenderers[file, rank].material.color = (IsLightSquare(file, rank)) ? lightColor : darkColor;
    }

    public void ResetSquareColors()
    {
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                SetSquareColor(file, rank, boardTheme.lightSquares.normal, boardTheme.darkSquares.normal);
            }
        }
    }
}
