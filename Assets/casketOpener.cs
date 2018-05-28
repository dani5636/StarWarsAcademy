using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casketOpener : MonoBehaviour {
    
    private Vector3 originalScale;

    private float limitMinScale = 0.1f;
    bool isScaling = false;

	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ScalingObject();
        if (Input.GetKeyDown(KeyCode.DownArrow))
        { 
            isScaling = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isScaling = false;
        }
    }

    private void ScalingObject()
    {


        if (isScaling && transform.localScale.y >= limitMinScale)
        {
            transform.localScale += new Vector3(0, -0.01f, 0);
        }


        if (!isScaling && transform.localScale != originalScale)
            {
                transform.localScale += new Vector3(0, 0.01f, 0);
            }
        
    }
}
