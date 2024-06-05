using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MasterController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Tile;
    public GameObject BishopBlack;

    public GameObject selectedPiece = null;

	void Start()
    {
        // Instantiating tiles in an 8x8 grid
        for (int i = -7; i < 8; i = i + 2)
        {
			for (int j = -7; j < 8; j = j + 2)
			{
                Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity);
			}
		}

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
