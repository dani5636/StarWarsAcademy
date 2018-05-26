using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeManager))]
public class GameManagerScript : MonoBehaviour {
    private bool gameStart;
    [SerializeField]
    private GameObject DangerSphere;
    private SphereDissolve dissolve;
    // Use this for initialization
    void Start () {
        dissolve = DangerSphere.GetComponent<SphereDissolve>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameStart) {
            if (!dissolve.running) {
                GetComponent<NodeManager>().gameStart = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;

                GetComponent<NodeManager>().TogglePause();
            }
            else {
                Time.timeScale = 1;
                GetComponent<NodeManager>().TogglePause();
                
            }
        }
	}
    public void StartGame() {
        dissolve.StartTriggerEffect();
        gameStart = true;
    }
    public void EndGame()
    {
        gameStart = true;
    }
}
