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

            if (other.GetType() == typeof(BoxCollider))
            {
                correctSlice = true;
                rotateBlade(other.transform.position);
                other.GetComponent<MoveNode>().speed = 0;
                other.gameObject.GetComponent<MoveNode>().OnHit(true);
                StartCoroutine("Cut", other.gameObject);

            }
            else if(other.GetType() !=typeof(BoxCollider)){

                other.GetComponent<MoveNode>().speed = 0;
                rotateBlade(other.transform.position);
                other.gameObject.GetComponent<MoveNode>().OnHit(false);
                StartCoroutine("Cut", other.gameObject);
            }

        }
        else if(other.tag.Equals("StartGame")){
            rotateBlade(other.transform.position);
            other.gameObject.GetComponent<Animation>().Stop();
            other.gameObject.GetComponent<StartCut>().OnHit();
            StartCoroutine("Cut", other.gameObject);
        }
    }
    void OnTriggerExit(Collider other) {
       

    }
    private IEnumerator Cut(GameObject node) {



        GameObject[] pieces = MeshCut.Cut(node, transform.position, transform.right, capMaterial);

        if (!pieces[1].GetComponent<Rigidbody>())
            pieces[1].AddComponent<Rigidbody>();

        pieces[0].GetComponent<Rigidbody>().isKinematic = false;
        
        if(pieces[0].gameObject.GetComponent<BoxCollider>() != null) { 
        pieces[0].gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (pieces[0].gameObject.GetComponent<MeshCollider>() != null)
        {
            pieces[0].gameObject.GetComponent<MeshCollider>().enabled = false;
        }
        if (pieces[0].gameObject.GetComponent<SphereCollider>() != null)
        {
            pieces[0].gameObject.GetComponent<SphereCollider>().enabled = false;
        }
        var forceZero = pieces[0].transform.position - transform.position;
        forceZero = forceZero.normalized;
        pieces[0].GetComponent<Rigidbody>().AddForce(forceZero * forceMultiplier);

        var forceOne = pieces[1].transform.position - transform.position;
        forceOne = forceZero.normalized;
        pieces[1].GetComponent<Rigidbody>().AddForce(forceOne * forceMultiplier);
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
    private void rotateBlade(Vector3 startDirection) {
        Vector3 pos = transform.position;
            Vector3 dir = startDirection - pos;
            Quaternion rotation = Quaternion.LookRotation(dir);
            rotation.x = 0;
            rotation.z = 0;
            transform.localRotation = rotation;
        
    }

}
