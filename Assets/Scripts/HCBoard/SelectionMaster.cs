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
		// Check when click
		if (Input.GetMouseButtonDown(0))
		{

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			// Check clicked object
			if (hit.collider != null)
			{
				Debug.Log("Clicked an object.");


				if (selectOne == null)
				{
					// Check if tile has a piece
					if (hit.collider.gameObject.GetComponent<TileSquare>().containedPiece != null)
					{
						// Selecting the first piece
						selectOne = hit.collider.gameObject;
						Debug.Log(selectOne.GetComponent<TileSquare>().containedPiece);
						selectOne.GetComponent<TileSquare>().lightOn();

					}
				}

				else if (selectTwo == null)
				{
					// Clicking same piece deselects it
					if ((selectOne != null) && (hit.collider.gameObject == selectOne))
					{
						selectOne.GetComponent<TileSquare>().lightOff();
						selectOne = null;
						selectTwo = null;
					}

					// Clicking different piece
					else
					{

						selectTwo = hit.collider.gameObject;
						selectOne.GetComponent<TileSquare>().lightOff();

						// If lands on a piece, destroy it
						if (selectTwo.GetComponent<TileSquare>().containedPiece != null)
						{
							Debug.Log("Capture!");
							Destroy(selectTwo.GetComponent<TileSquare>().containedPiece.gameObject);
							Destroy(selectTwo.GetComponent<TileSquare>().containedPiece);
							
						}

						selectOne.GetComponent<TileSquare>().containedPiece.transform.position = selectTwo.transform.position;
						selectTwo.GetComponent<TileSquare>().containedPiece = selectOne.GetComponent<TileSquare>().containedPiece;
						selectOne.GetComponent<TileSquare>().containedPiece = null;
						selectOne = null;
						selectTwo = null;
					}

				}

				



			}

			else
			{
				// Clicking nothing deselects everything
				if (selectOne != null)
				{
					selectOne.GetComponent<TileSquare>().lightOff();
				}
					
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
