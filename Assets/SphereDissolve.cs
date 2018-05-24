using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDissolve : MonoBehaviour {


    public Material dissolveSphereMaterial, dissolveArchMaterial;

    public float speed, max, min;
    public bool dissolving, running;
    public float currentY;
    private void Start(){
        dissolveSphereMaterial.SetFloat("_DissolveY", currentY);
    }
    private void Update()
    {
        if (running) {
            if (dissolving && currentY > min)
            {
                dissolveSphereMaterial.SetFloat("_DissolveY", currentY);
                dissolveArchMaterial.SetFloat("DissolveY", currentY);
                currentY -= Time.deltaTime * speed;
            }
            else if (!dissolving && currentY < max)
            {
                dissolveSphereMaterial.SetFloat("_DissolveY", currentY);
                dissolveArchMaterial.SetFloat("DissolveY", currentY);
                currentY += Time.deltaTime * speed;
            }
            else {
                running = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            running = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartTriggerEffect();
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EndTriggerEffect();

        }

    }

    public void StartTriggerEffect()
    {
        dissolving = true;
        running = true;
    }
    public void EndTriggerEffect() {
        dissolving = false;
        running = true;
        
    }

    private void ResetEffect()
    {

        dissolving = true;

    }
}
