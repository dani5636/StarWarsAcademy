using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTriggerEffect : MonoBehaviour {

    public Material dissolveMaterial;
    public float speed, max;

    private float currentY, startTime;

    private void Update()
    {
        if (currentY < max)
        {
            dissolveMaterial.SetFloat("_DissolveY", currentY);
            currentY -= Time.deltaTime * speed;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerEffect();
        } 

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetEffect();

        }
    }

    private void TriggerEffect()
    {
        startTime = Time.time;
        currentY = -15;
        max = 100;
    }

    private void ResetEffect()
    {

        currentY = -15;
        max = -100;

    }
}
