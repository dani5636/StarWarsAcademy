using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PointsScript : MonoBehaviour {

    public static int scoreValue = 0;
    TextMeshProUGUI points;

    void Start() {

        points = GetComponent<TextMeshProUGUI>();

    }


    void Update() {
        points.text = "" + scoreValue;

        if (Input.GetKeyDown(KeyCode.T))
        {
            scoreValue += 10;
        }
    }

    
}
