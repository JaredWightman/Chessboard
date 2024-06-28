using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject startSquare;
    public GameObject endSquare;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Clicked object.");

                if (startSquare == null)
                {
                    if (hit.collider.gameObject.GetComponent<TileSquare>().containedPiece != null)
					{
						// Selecting the first piece
						startSquare = hit.collider.gameObject;
						Debug.Log(startSquare.GetComponent<TileSquare>().containedPiece);
						startSquare.GetComponent<TileSquare>().lightOn();

					}
                }
            }
        }
    }
}
