using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitToMenuScript : MonoBehaviour {

    GameObject levelChanger;
    // Use this for initialization
    void Start() {

    }
    public void SetLevelChanger(GameObject level) {
        levelChanger = level;
    }
    public void OnHit()
    {
        VRControllerInput.SABER_SELECTED = false;
        levelChanger.GetComponent<LevelChanger>().ChangeLevel();   
    }
	
	

}
