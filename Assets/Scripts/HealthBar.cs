using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Image healthBar;
    private float speed = 0.03f;
    private bool running;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float startHp;

    void Start () {

        healthBar = GetComponent<Image>();

        healthBar.fillAmount = startHp;

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
    public void SetRunning(bool running) {
        this.running = running;
    }
    public float GetHealth() {
        return healthBar.fillAmount;
    }
    public void ResetHealth()
    {
        healthBar.fillAmount = startHp;
    }
}
