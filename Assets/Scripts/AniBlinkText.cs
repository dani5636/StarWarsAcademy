using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class AniBlinkText : MonoBehaviour
{

    [SerializeField]
    private float minAlpha = 0.2f;
    [SerializeField]
    private float maxAlpha = 0.8f;
    private bool isIncreasing = false;
    private TextMeshPro txt;
    private Color col;

	// Use this for initialization
	void Start () {

        txt = GetComponent<TextMeshPro>();
        col = txt.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        IncreaseCheck();
        if(isIncreasing){
            col.a += +0.4f * Time.deltaTime;
        }
        else{
            col.a += -0.4f * Time.deltaTime;
        }
        txt.color = col;
    }

    private void IncreaseCheck()
    {
        if (col.a < minAlpha && !isIncreasing)
        {
            isIncreasing = true;
        }
        else if (col.a > maxAlpha && isIncreasing)
        {
            isIncreasing = false;
        }
    }
}
