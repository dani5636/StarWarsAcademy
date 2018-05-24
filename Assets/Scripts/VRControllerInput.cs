using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerInput : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    [SerializeField]
    GameObject blade;
    // 2
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    void Update() {
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        // 2
        if (Controller.GetHairTriggerDown())
        {
            if(blade!=null)
                blade.GetComponent<Lightsaber>().ToogleOnOffLightsaber(true);
        }

        // 3
        if (Controller.GetHairTriggerUp())
        {
            if (blade != null)
                blade.GetComponent<Lightsaber>().ToogleOnOffLightsaber(false);
        }
    }
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
}
