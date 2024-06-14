using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// This is the controller for instantiated pieces, in GenScene.
public class MasterController : MonoBehaviour
{

    public GameObject Tile; // TileSquare
	public GameObject selectedPiece = null;
	public GameObject selectedDestination = null;
	public bool isWhiteTurn = true; // Swap these with every turn
    public bool isBlackTurn = false;

	public GameObject BishopBlack;
    private Dictionary<Vector3, GameObject> tileList;

	void Start()
    {
        // Instantiating tiles in an 8x8 grid. DON'T HAVE TO INSTANTIATE IF WE JUST HARD-CODE THE TILES/PIECES

        for (int i = -7; i <= 7; i = i + 2)
        {
			for (int j = -7; j <= 7; j = j + 2)
			{

                var spawnedTile = Instantiate(Tile, new Vector3(i, j, -1), Quaternion.identity);
                
                

                tileList[new Vector3(i, j)] = spawnedTile; // Can't use spawnedTile, not set to an instance of an object?
			}
		}
        
		var pieceboi = Instantiate(BishopBlack, new Vector3(-3, 7, 0), Quaternion.identity);
		Debug.Log(pieceboi.GetComponent<BasicPiece>().tileUnderneath); // Calling a variable
		var temporaryTile = Instantiate(Tile, new Vector3(-5, 7, -2), Quaternion.identity);
        temporaryTile.GetComponent<TileSquare>().lightOn();

		Vector3 position = new Vector3(-3, 7, 0);
        GameObject tileboi = getTile(position);
        Debug.Log("Here is what I got:");
        //Debug.Log(tileboi);
        //pieceboi.GetComponent<TileSquare>();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getTile(Vector3 position)
    {
		if (tileList.TryGetValue(position, out GameObject tile))
        {
			Debug.Log("Got an object");
			return null;
		}
        else
        {
            Debug.Log("No object");
            return null;
        }
	}
}
