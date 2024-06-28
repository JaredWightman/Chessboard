using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMaster : MonoBehaviour
{
	public GameObject selectOne;
	public GameObject selectTwo;

	void Start()
    {
        
    }


    void Update()
    {
		// Put this in update
		if (Input.GetMouseButtonDown(0))
		{

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit.collider != null)
			{
				// Clicking something
				Debug.Log("You just clicked something: " + hit.collider.name);

				// Check if tile has a piece
				if (hit.collider.gameObject.GetComponent<TileSquare>().containedPiece != null)
				{
					// Clicking same piece deselects it
					if ((selectOne != null) && (hit.collider.gameObject == selectOne))
					{
						selectOne.GetComponent<TileSquare>().lightOff();
						selectOne = null;
						selectTwo = null;
					}

					// Clicking the first piece
					else if (selectOne == null)
					{
						selectOne = hit.collider.gameObject;
						selectOne.GetComponent<TileSquare>().lightOn();
					}

					// Clicking second piece
					else
					{
						selectTwo = hit.collider.gameObject;
						// Check if valid, do logic, move to other player's turn, then clear out selections
						selectOne.GetComponent<TileSquare>().lightOff();

					}
				}
			}

				
			else
			{
				// Clicking nothing deselects everything
				Debug.Log("Click on something, dingus");
				selectOne = null;
				selectTwo = null;
			}



			// DEBUG STATEMENTS

			if (selectOne != null)
			{
				Debug.Log("First piece seleced:" + selectOne.name);
			}
			else
			{
				Debug.Log("First piece is null.");
			}

			if (selectTwo != null)
			{
				Debug.Log("Second piece selected:" + selectTwo.name);
			}
			else
			{
				Debug.Log("Second piece is null.");
			}


		}



	}
}
