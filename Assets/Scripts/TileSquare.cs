using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.U2D;

public class TileSquare : MonoBehaviour
{
	// Start is called before the first frame update

	private SpriteRenderer renderer;
	public GameObject containedPiece = null;
	public bool isLitUp = false;
	public bool isSelectable = true; // All are selectable for now. Change this later.
	public bool isSelected = false;

	public void lightOn()
	{
		renderer.color = new Color(255, 255, 0, 0.3f);
		isLitUp = true;
	}
	public void lightOff()
	{
		renderer.color = new Color(255, 255, 255, 0);
		isLitUp = false;
	}


	void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update(){}

	void OnMouseOver()
	{
		//lightOn();
		if (isSelectable && !isSelected && !isLitUp) // Check doesn't work yet
		{
			lightOn();
		}
	}

	void OnMouseExit()
	{
		if (isSelectable && !isSelected && isLitUp) // CHeck doesn't work yet
		{
			lightOff();
		}
	}

	private void OnMouseUp()
	{
		if (isSelectable && !isSelected)
		{
			isSelected = true;
		}
		else if (isSelectable && isSelected)
		{
			isSelected = false;
		}
		

	}

}
