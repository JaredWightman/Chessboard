using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// This is the controller for instantiated pieces, in GenScene.
public class MasterController : MonoBehaviour
{

    public GameObject Tile;
    public GameObject selectedPiece = null;
    public bool isWhiteTurn = true; // Swap these with every turn
    public bool isBlackTurn = false;

	public GameObject BishopBlack;

	void Start()
    {
        // Instantiating tiles in an 8x8 grid. DON'T HAVE TO INSTANTIATE IF WE JUST HARD-CODE THE TILES/PIECES
        for (int i = -7; i <= 7; i = i + 2)
        {
			for (int j = -7; j <= 7; j = j + 2)
			{
                Instantiate(Tile, new Vector3(i, j, -1), Quaternion.identity);
			}
		}
        // Creating a bishop
		Instantiate(BishopBlack, new Vector3(-3, 7, 0), Quaternion.identity);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
