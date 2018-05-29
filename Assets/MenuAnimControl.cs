using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class MenuAnimControl : MonoBehaviour {
    private Animation anim;
    public bool shouldAnim = true;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animation>();
        anim.Play("Appear");
	}
	
	// Update is called once per frame
	void Update () {
        if (!anim.isPlaying && shouldAnim) {
            anim.Play("HoverRotateStart");
        }
	}
}
