using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    [SerializeField]
    private GameObject levelChange;
    private LevelChanger levelScript;
    private bool levelChangeInProgress = false;
    // 2
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        levelScript = levelChange.GetComponent<LevelChanger>();
    }
    void Update()
    {
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        // 2
        if (Controller.GetHairTriggerDown() && !levelChangeInProgress)
        {
            levelScript.ChangeLevel();
            levelChangeInProgress = true;
        }

        // 3
        if (Controller.GetHairTriggerUp())
        {

        }
    }
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
}
