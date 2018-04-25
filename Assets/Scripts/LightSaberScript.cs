using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class LightSaberScript : MonoBehaviour
{
    [SerializeField] private float minSaberSize = 0.01f;
    [SerializeField] private float maxSaberSize = 0.7f;
    [SerializeField] private float speedFactor = 1.0f;
    [SerializeField] private GameObject blade;

    private bool isActivated = false;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    ActivateLightSaber();
	    
    }

    private void ActivateLightSaber()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isActivated = true;
            blade.GetComponent<CuttingBlade>().setActive(isActivated);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            isActivated = false;
            blade.GetComponent<CuttingBlade>().setActive(isActivated);
        }
        if (transform.localScale.y >= minSaberSize && !isActivated)
        {
            transform.localScale += Vector3.down * (Time.deltaTime * speedFactor);

        }
        else if (transform.localScale.y <= maxSaberSize && isActivated)
        {
            transform.localScale += Vector3.up * (Time.deltaTime * speedFactor);

        }
    }
}
