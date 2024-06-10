using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.U2D;

public class TileSquare : MonoBehaviour
{

	private SpriteRenderer renderer;
	public GameObject containedPiece = null;
	public bool isLitUp = false;
	public bool isSelectable = false;
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
		isSelectable = true; // DELETE THIS LATER
	}
    void Update(){}

	void OnMouseOver()
	{

		if (isSelectable && !isSelected && !isLitUp)
		{
			lightOn();
		}
	}

	void OnMouseExit()
	{
		if (isSelectable && !isSelected && isLitUp)
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
