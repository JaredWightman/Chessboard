using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Theme/Board")]
public class BoardTheme : ScriptableObject
{
    public SquareColors lightSquares;
    public SquareColors darkSquares;

    [System.Serializable]
    public struct SquareColors {
        public Color normal;
        public Color legal;
        public Color selected;
        public Color moveFromHighlight;
        public Color moveToHighlight;
    }
}
