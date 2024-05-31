using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPiece : MonoBehaviour
{

    bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown() // Click event
    {
        if (isSelected)
        {
            isSelected = false;
            Debug.Log("Deselected!");
        }
        else
        {
            isSelected = true;
            Debug.Log("Selected!");
            //Collision2D collision = GetComponent<Collision2D>(); //This doesn't work
            //collision.gameObject.GetComponent<TileSquare>().lightUp();
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Trigger works!");
		//collision.gameObject.GetComponent<TileSquare>().lightUp();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("Collider works!");
	}
}
