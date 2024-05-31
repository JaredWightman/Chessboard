using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPiece : MonoBehaviour
{

    bool isSelected = false;
    //public Transform transform;
    public float xPos;
    public float yPos;


    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
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
            // Collision2D collision = GetComponent<Collision2D>(); //This doesn't work
            // collision.gameObject.GetComponent<TileSquare>().lightUp();
        }
    }
}
