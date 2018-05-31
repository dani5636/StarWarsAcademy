using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPlayerScript : MonoBehaviour {
    private bool ShouldFly = false;
    private GameObject destination;
    private float proximity = 0.3f;
    private float speed = 2.0f;
    // Use this for initialization


    // Update is called once per frame
    void Update () {
        if (ShouldFly && !IsClose())
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, step);
        }
        else if (ShouldFly && IsClose())
        {
            destination.GetComponent<VRControllerInput>().SwitchControls();
            Destroy(GameObject.FindGameObjectWithTag("CasketHolder"));
            Destroy(gameObject);
        }
    }

    public void SetDestination(GameObject des) {
        gameObject.transform.parent = null;

        destination = des;
        ShouldFly = true;
    }
    private bool IsClose() {
        if (destination != null) {
            float distance = Vector3.Distance(gameObject.transform.position, destination.transform.position);
            if (distance < proximity)
            {
                return true;
            }

        }
        return false;
    }
}
