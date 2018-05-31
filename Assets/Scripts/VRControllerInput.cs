using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerInput : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    [SerializeField]
    public GameObject hilt;
    [SerializeField]
    GameObject glassController;

    [SerializeField]
    Material blue;

    [SerializeField]
    Material red;

    [SerializeField]
    Material white;

    [SerializeField]
    Material green;
    [SerializeField]
    GameObject otherLightsaber;
    static GameObject staticGlassController;
   public static bool SABER_SELECTED = false;
    bool on = false;
    // 2
    void Start()
    {
        if (glassController != null && staticGlassController == null) {
            staticGlassController = glassController;
        }   
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    private void ChangeColor(Material one, Material two) {

        //Trigger Controller
        hilt.transform.Find("Blade").gameObject.GetComponent<MeshRenderer>().material = one;
        hilt.transform.Find("Blade").transform.Find("BladeTrail").gameObject.GetComponent<MeshRenderer>().material = one;
        //OtherController
        GameObject otherHilt = otherLightsaber.GetComponent<VRControllerInput>().hilt;
        otherHilt.transform.Find("Blade").gameObject.GetComponent<MeshRenderer>().material = two;
        otherHilt.transform.Find("Blade").transform.Find("BladeTrail").gameObject.GetComponent<MeshRenderer>().material = two;
    }
    void Update() {
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        // 2
        if (Controller.GetHairTriggerDown())
        {
            //Controller(right)
            if (!SABER_SELECTED) {
                if (gameObject.name.Equals("Controller (right)")) {

                    staticGlassController.GetComponent<GlassController>().EvilSide(gameObject, GameObject.Find("Controller (left)"));
                    ChangeColor(white, red);
                    SABER_SELECTED = true;
                }
                else
                {
                    staticGlassController.GetComponent<GlassController>().GoodSide(GameObject.Find("Controller (right)"), gameObject);
                    //left
                    ChangeColor(blue, green);

                    SABER_SELECTED = true;

                }
            }
            else {

            if (on && hilt.activeInHierarchy)
            {
                on = false;
            }
            else if (hilt.activeInHierarchy)
                {
                on = true;
             }
                if (hilt != null)
                { 
                hilt.GetComponent<Lightsaber>().ToogleOnOffLightsaber(on);
                }
            }
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
    public void SwitchControls()
    {
        if(gameObject)
        gameObject.transform.Find("Model").gameObject.SetActive(false);
        gameObject.transform.Find("LightsaberJoint").gameObject.SetActive(true);
        GameObject[] holoDiscs = GameObject.FindGameObjectsWithTag("HoloChoice");
        foreach (GameObject holo in holoDiscs) {
            Destroy(holo);
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().ReadyMenu();

    }

}
