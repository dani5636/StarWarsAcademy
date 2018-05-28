using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Image healthBar;
    private float speed = 0.03f;
    private bool running;
 

    void Start () {

        healthBar = GetComponent<Image>();

        healthBar.fillAmount = 0.5f;

        running = false;
    }
	
	
	void Update () {

        if (running)
        {
            
            DecreasingHealth();

        }
        
       



    }
    private void DecreasingHealth()
    {
        healthBar.fillAmount -= Time.deltaTime * speed;
    }

    public void HealthIncrease()
    {
        healthBar.fillAmount += 0.05f;
    }
    public void HealthDecrease() {

        healthBar.fillAmount -= 0.05f;
    }
    public void setRunning(bool running) {
        this.running = running;
    }
    public float getHealth() {
        return healthBar.fillAmount;
    }
    public void GameOver()
    {
        if (healthBar.fillAmount == 0)
        {

        }

    }
}
