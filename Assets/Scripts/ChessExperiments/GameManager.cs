using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum InputState {
        None,
        PieceSelected,
        DraggingPiece
    }

    InputState currentState;

    public BoardUI boardUI;
    public Camera cam;
    Coord selectedPieceSquare;
    Board board = new Board();
    /*public GameManager (Board board) {
        boardUI = GameObject.FindObjectOfType<BoardUI> ();
        cam = Camera.main;
        this.board = board;
    }*/

    public void Start()
    {
        boardUI.UpdatePosition(board);
    }

    public void NotifyTurnToMove () {

    }

    public void Update () {
        HandleInput ();
    }

    void HandleInput () {
        Vector2 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

        if (currentState == InputState.None) {
            HandlePieceSelection (mousePos);
        } else if (currentState == InputState.DraggingPiece) {
            HandleDragMovement (mousePos);
        } else if (currentState == InputState.PieceSelected) {
            HandlePointAndClickMovement (mousePos);
        }

        if (Input.GetMouseButtonDown (1)) {
            CancelPieceSelection ();
        }
    }

    void HandlePointAndClickMovement (Vector2 mousePos) {
        if (Input.GetMouseButton (0)) {
            HandlePiecePlacement (mousePos);
        }
    }

    void HandleDragMovement (Vector2 mousePos) {
        boardUI.DragPiece (selectedPieceSquare, mousePos);
        // If mouse is released, then try place the piece
        if (Input.GetMouseButtonUp (0)) {
            HandlePiecePlacement (mousePos);
        }
    }
    

    void HandlePiecePlacement (Vector2 mousePos) {
        Coord targetSquare;
        if (boardUI.TryGetSquareUnderMouse (mousePos, out targetSquare)) {
            if (targetSquare.Equals (selectedPieceSquare)) {
                boardUI.ResetPiecePosition (selectedPieceSquare);
                if (currentState == InputState.DraggingPiece) {
                    currentState = InputState.PieceSelected;
                } else {
                    currentState = InputState.None;
                    boardUI.DeselectSquare (selectedPieceSquare);
                }
            } else {
                //int targetIndex = BoardRepresentation.IndexFromCoord (targetSquare.fileIndex, targetSquare.rankIndex);
                if (board.GetSquares()[targetSquare.fileIndex, targetSquare.rankIndex].GetColor() == board.colorToMove && board.GetSquares()[targetSquare.fileIndex, targetSquare.rankIndex].GetPiece() != Piece.None) {
                    CancelPieceSelection ();
                    HandlePieceSelection (mousePos);
                } else {
                    TryMakeMove (selectedPieceSquare, targetSquare);
                }
            }
        } else {
            CancelPieceSelection ();
        }

    }

    void CancelPieceSelection () {
        if (currentState != InputState.None) {
            currentState = InputState.None;
            boardUI.DeselectSquare (selectedPieceSquare);
            boardUI.ResetPiecePosition (selectedPieceSquare);
        }
    }

    void TryMakeMove (Coord startSquare, Coord targetSquare) {
        bool moveIsLegal = board.IsMoveLegal(startSquare, targetSquare);

        if (moveIsLegal) {
            currentState = InputState.None;
            board.MakeMove(startSquare, targetSquare);
            //boardUI.UpdatePosition(board);
            //boardUI.DeselectSquare(selectedPieceSquare);
            boardUI.OnMoveMade(board, startSquare, targetSquare);
        } else {
            CancelPieceSelection ();
        }
    }

    void HandlePieceSelection (Vector2 mousePos) {
        if (Input.GetMouseButtonDown (0)) {
            if (boardUI.TryGetSquareUnderMouse (mousePos, out selectedPieceSquare)) {
                //int index = BoardRepresentation.IndexFromCoord (selectedPieceSquare);
                // If square contains a piece, select that piece for dragging
                if (board.GetSquares()[selectedPieceSquare.fileIndex, selectedPieceSquare.rankIndex].GetColor() == board.colorToMove) {
                    //boardUI.HighlightLegalMoves (board, selectedPieceSquare);
                    boardUI.SelectSquare (selectedPieceSquare);
                    currentState = InputState.DraggingPiece;
                    boardUI.HighlightLegalMoves(board.GenerateMoves(selectedPieceSquare));
                }
            }
        }
    }
}
