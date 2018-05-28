using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCut : MonoBehaviour {
    [SerializeField]
    private GameObject gameManager;
    GameManagerScript gameScript;
	// Use this for initialization
	void Start () {
        gameScript = gameManager.GetComponent<GameManagerScript>();
	}
    public void OnHit() {
        gameScript.StartGame();
    }
	
	
}
