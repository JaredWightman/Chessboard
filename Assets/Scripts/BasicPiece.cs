using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPiece : MonoBehaviour
{

    bool isSelected = false;
    public float xPos;
    public float yPos;
    public GameObject tileUnderneath = null;


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
        }
    }
}
