using BLINDED_AM_ME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBlade : MonoBehaviour {

    [SerializeField]
    private Material capMaterial;
    [SerializeField]
    private float forceMultiplier = 10.0f;
    private bool isActive;
    private bool isCutting;
    private Vector3 startCuttingPosition;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Node") && isActive)
        {
            if (other.gameObject.GetComponent<MoveNode>().hitable) { 
            startCuttingPosition = transform.position;
            isCutting = true;
            }

        }
    }
    void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Node") && isCutting)
        {

            other.gameObject.GetComponent<MoveNode>().OnHit();
            GameObject victim = other.gameObject;
            Vector3 cuttingDirection = startCuttingPosition - transform.position;
            cuttingDirection = cuttingDirection.normalized;
            GameObject[] pieces = MeshCut.Cut(victim, startCuttingPosition, transform.right, capMaterial);
        
            if (!pieces[1].GetComponent<Rigidbody>())
                pieces[1].AddComponent<Rigidbody>();

            pieces[0].GetComponent<Rigidbody>().isKinematic = false;

            other.gameObject.GetComponent<MoveNode>().speed = 0;
            pieces[0].GetComponent<Rigidbody>().AddForce((Vector3.forward + Vector3.left)*forceMultiplier);



        }
    }

    public void setActive(bool active)
    {
        isActive = active;
    }


}
