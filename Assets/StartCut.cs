using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCut : MonoBehaviour {
    [SerializeField]
    private GameObject gameManager;
    GameManagerScript gameScript;
	// Use this for initialization
	void Start () {
	}
    public void OnHit()
    {
        gameScript.StartGame();
        gameObject.GetComponent<MenuAnimControl>().shouldAnim = false;


    
    }
    public void SetScript(GameManagerScript game) {
        gameScript = game;
    }
	
	
}
