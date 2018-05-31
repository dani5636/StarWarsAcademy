using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasketOpener : MonoBehaviour {

    private Vector3 originalScale;

    [SerializeField]
    private GameObject animholder;

    [SerializeField]
    private GameObject soundEffect;

    private AudioSource musicSource;

    [SerializeField]
    private GameObject lightSaber;
    private float limitMinScale = 0.1f;
    public bool isScaling = false;

    public bool selectionDone;
    [SerializeField]
    private float moveSpeed = 0.3f;
    GameObject destination;
    bool hasStartedAudio = false;

	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
        musicSource = soundEffect.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ScalingObject();
    

    }
    public void SetDestination(GameObject destination) {

        this.destination = destination;
            }
    private void ScalingObject()
    {


        if (isScaling && transform.localScale.y >= limitMinScale)
        {
            if (!hasStartedAudio)
            {
                musicSource.Play();
                hasStartedAudio = true;
            }
            transform.localScale += new Vector3(0, -0.01f, 0);
        } else if (isScaling ||  selectionDone)
        {
            if (destination != null && !selectionDone) {

                lightSaber.GetComponent<FlyToPlayerScript>().SetDestination(destination);
                selectionDone = true;
            }
            animholder.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }


        if (!isScaling && transform.localScale != originalScale)
            {
                transform.localScale += new Vector3(0, 0.01f, 0);
            }
        
    }
}
