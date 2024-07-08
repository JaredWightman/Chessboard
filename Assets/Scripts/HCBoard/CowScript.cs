using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : MonoBehaviour
{
    // Start is called before the first frame update

    int counter = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This is a frame-based time delay, not useful on different machines but I couldn't be bothered to make a new function
        counter++;
        if (counter == 1000)
        {
            counter = 0;
            if (transform.position == new Vector3(0, 0, 0))
            {
				transform.position = new Vector3(0, 1, 0);
			}
            else
            {
                transform.position = new Vector3(0, 0, 0);
			}
		}
    }
}
