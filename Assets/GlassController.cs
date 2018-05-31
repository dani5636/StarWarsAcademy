using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassController : MonoBehaviour {
    [SerializeField]
    GameObject greenGlass;
    [SerializeField]
    GameObject blueGlass;
    [SerializeField]
    GameObject redGlass;
    [SerializeField]
    GameObject whiteGlass;
    
    // Use this for initialization

    public void GoodSide(GameObject right, GameObject left)
    {
        greenGlass.GetComponent<CasketOpener>().SetDestination(right);
        blueGlass.GetComponent<CasketOpener>().SetDestination(left);
        blueGlass.GetComponent<CasketOpener>().isScaling = true;
        greenGlass.GetComponent<CasketOpener>().isScaling = true;
        redGlass.GetComponent<CasketOpener>().selectionDone = true;
        whiteGlass.GetComponent<CasketOpener>().selectionDone = true;
    }
    public void EvilSide(GameObject right, GameObject left)
    {
        whiteGlass.GetComponent<CasketOpener>().SetDestination(right);
        redGlass.GetComponent<CasketOpener>().SetDestination(left);
        redGlass.GetComponent<CasketOpener>().isScaling = true;
        whiteGlass.GetComponent<CasketOpener>().isScaling = true;
        blueGlass.GetComponent<CasketOpener>().selectionDone = true;
        greenGlass.GetComponent<CasketOpener>().selectionDone = true;
    }
}
