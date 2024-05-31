using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TileSquare : MonoBehaviour
{
	// Start is called before the first frame update

	private SpriteRenderer renderer;

	public void lightUp()
	{
		renderer.color = new Color(255, 255, 0, 0.3f);
		print("Lighting Up!");
	}
	void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update(){}

	void OnMouseOver()
	{
		//renderer.color = new Color(255, 255, 0, 0.3f);
	}

	void OnMouseExit()
	{
		renderer.color = new Color(255, 255, 255, 0);
	}

}
