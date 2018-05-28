using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casketOpener : MonoBehaviour {
    
    private Vector3 originalScale;

    [SerializeField]
    private GameObject animholder;

    [SerializeField]
    private GameObject soundEffect;

    private AudioSource musicSource;

    private float limitMinScale = 0.1f;
    bool isScaling = false;

    [SerializeField]
    private float moveSpeed = 0.3f;

	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
        musicSource = soundEffect.GetComponent<AudioSource>();
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
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
            transform.localScale += new Vector3(0, -0.01f, 0);
        } else if (isScaling)
        {
            animholder.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }


        if (!isScaling && transform.localScale != originalScale)
            {
                transform.localScale += new Vector3(0, 0.01f, 0);
            }
        
    }
}
