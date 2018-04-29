using BLINDED_AM_ME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBlade : MonoBehaviour {

    [Header("Inner Material")]
    [SerializeField]
    private Material capMaterial;
    [SerializeField]
    private float forceMultiplier = 10.0f;
    private bool isActive;
    private bool isCutting;
    private bool correctSlice = false;
    private Vector3 startCuttingPosition;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Node") && isActive && other.gameObject.GetComponent<MoveNode>().hitable )
        {

            if (other.GetType() == typeof(BoxCollider) && !isCutting)
            {
                correctSlice = true;
            }
            else if(other.GetType() !=typeof(BoxCollider) && !isCutting){
                correctSlice = false;
            }
            startCuttingPosition = transform.position;
            isCutting = true;

        }
    }
    void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Node") && isCutting && other.GetType() != typeof(BoxCollider))
        {

            StartCoroutine("Cut", other.gameObject);
            other.gameObject.GetComponent<MoveNode>().OnHit(correctSlice);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<MeshCollider>().enabled = false;
            isCutting = false;
            correctSlice = false;
        }
    }
    private IEnumerator Cut(GameObject node) {
        Vector3 cuttingDirection = startCuttingPosition - transform.position;
        cuttingDirection = cuttingDirection.normalized;
        node.GetComponent<MoveNode>().speed = 0;
        GameObject[] pieces = MeshCut.Cut(node, startCuttingPosition, transform.right, capMaterial);

        if (!pieces[1].GetComponent<Rigidbody>())
            pieces[1].AddComponent<Rigidbody>();

        pieces[0].GetComponent<Rigidbody>().isKinematic = false;
       
        pieces[0].GetComponent<Rigidbody>().AddForce((((Vector3.left / 2) + Vector3.forward) - cuttingDirection) * forceMultiplier);
        pieces[1].GetComponent<Rigidbody>().AddForce((((Vector3.right / 2) + Vector3.forward) - cuttingDirection) * forceMultiplier);

        foreach (GameObject piece in pieces)
        {
            Destroy(piece, 2.0f);
        }
        yield return null;
    }
    public void SetActive(bool active)
    {
        isActive = active;
    }


}
