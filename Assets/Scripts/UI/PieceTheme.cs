using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Theme/Pieces")]
public class PieceTheme : ScriptableObject
{
    public PieceSprites whitePieces;
    public PieceSprites blackPieces;

    public Sprite GetPieceSprite (int piece, int color)
    {        
        switch (piece) {
            case Piece.Pawn:
                if (color == Piece.White) {
                    return whitePieces.Pawn;
                } else {
                    return blackPieces.Pawn;
                }
            case Piece.Rook:
                if (color == Piece.White) {
                    return whitePieces.Rook;
                } else {
                    return blackPieces.Rook;
                }
            case Piece.Knight:
                if (color == Piece.White) {
                    return whitePieces.Knight;
                } else {
                    return blackPieces.Knight;
                }
            case Piece.Bishop:
                if (color == Piece.White) {
                    return whitePieces.Bishop;
                } else {
                    return blackPieces.Bishop;
                }
            case Piece.Queen:
                if (color == Piece.White) {
                    return whitePieces.Queen;
                } else {
                    return blackPieces.Queen;
                }
            case Piece.King:
                if (color == Piece.White) {
                    return whitePieces.King;
                } else {
                    return blackPieces.King;
                }
            default:
                return null;
        }
    }

    [System.Serializable]
    public class PieceSprites {
        public Sprite King, Queen, Rook, Bishop, Knight, Pawn;
    }
}
