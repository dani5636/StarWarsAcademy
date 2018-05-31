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
    [SerializeField]
    private GameObject audioSwing;
    private bool isActive;
    private bool isCutting;
    private bool correctSlice = false;
    private Vector3 startCuttingPosition;
    private Vector3 lastPosition;
    private Vector3 lastUpperPosition;
    private Vector3 lastMidPosition;
    

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPosition = transform.position;
        Vector3 currentUpperLimit = transform.TransformPoint(transform.localPosition + (Vector3.up * transform.localScale.y));
        Vector3 currentMidLimit = transform.TransformPoint(transform.localPosition + (Vector3.up * (transform.localScale.y / 2)));
        if (lastPosition != null) {
            GameObject cuttable = null;
            float maxDistance = Vector3.Distance(lastPosition, currentPosition);
            float maxDistanceUpper = Vector3.Distance(lastUpperPosition, currentUpperLimit);
            float maxDistanceMid = Vector3.Distance(lastMidPosition, currentMidLimit);
            RaycastHit hit;
            Debug.DrawRay(lastPosition, currentPosition - lastPosition, Color.red, .5f);
            Debug.DrawRay(lastUpperPosition, currentUpperLimit - lastUpperPosition, Color.red, .5f);
            Debug.DrawRay(lastMidPosition, currentMidLimit - lastMidPosition, Color.red, .5f);
            if (Physics.Raycast(lastPosition, currentPosition - lastPosition, out hit, maxDistance))
            {
                Debug.Log(""+ hit.collider.gameObject.name);
                cuttable = hit.collider.gameObject;
            }
            else if (Physics.Raycast(lastUpperPosition, currentUpperLimit - lastUpperPosition, out hit, maxDistanceUpper))
            {
                cuttable = hit.collider.gameObject;
            }
            else if (Physics.Raycast(lastMidPosition, currentMidLimit - lastMidPosition, out hit, maxDistanceMid)) {
                cuttable = hit.collider.gameObject;
            }
            if (cuttable != null)
            {
                CutEvent(cuttable, hit.collider, maxDistance, currentMidLimit, lastMidPosition, lastUpperPosition);
            }



        }
        lastPosition = currentPosition;
        lastMidPosition = currentMidLimit;
        lastUpperPosition = currentUpperLimit;
    }


    public void CutEvent(GameObject cuttable, Collider col, float force, Vector3 a, Vector3 b, Vector3 c)
    {
        if (!audioSwing.GetComponent<AudioSource>().isPlaying) {
            audioSwing.GetComponent<AudioSource>().Play();
        }
        if (cuttable.tag.Equals("Node") && isActive && cuttable.GetComponent<MoveNode>().hitable)
        {
            MoveNode script = cuttable.GetComponent<MoveNode>();
            script.speed = 0;
            if (col.GetType() == typeof(BoxCollider)) {

                script.OnHit(true);
            }
            else if (col.GetType() != typeof(BoxCollider))
            {
                script.OnHit(false);
            }
            foreach (Collider coll in cuttable.GetComponents<Collider>()) {
                coll.enabled = false;
            }

            StartCoroutine(Cut(cuttable, force, a, b, c));

        }
        else if (cuttable.tag.Equals("StartGame")) {

            cuttable.GetComponent<Animation>().Stop();
            if (cuttable.GetComponent<StartCut>() != null)
            {
                cuttable.GetComponent<StartCut>().OnHit();
            }
            if (cuttable.GetComponent<QuitToMenuScript>() != null)
            {
                cuttable.GetComponent<QuitToMenuScript>().OnHit();
            }
            foreach (Collider coll in cuttable.GetComponents<Collider>())
            {
                coll.enabled = false;
            }

            StartCoroutine(Cut(cuttable, force,a,b,c));

        }
        

    }


    void OnTriggerEnter(Collider col)
    {

        Vector3 currentMidLimit = transform.TransformPoint(transform.localPosition + (Vector3.up * (transform.localScale.y / 2)));
        float distance = Vector3.Distance(currentMidLimit, lastMidPosition);

        CutEvent(col.gameObject, col, distance, currentMidLimit, lastMidPosition, lastUpperPosition);

    }
    private IEnumerator Cut(GameObject node, float force, Vector3 a, Vector3 b, Vector3 c) {

        var side1 = b - a;
        var side2 = c - a;
        var normal = Vector3.Cross(side1, side2);
        var turnToNormal = Quaternion.FromToRotation(Vector3.forward, Vector3.right);

        GameObject[] pieces = MeshCut.Cut(node, transform.position,  normal, capMaterial);

        if (!pieces[1].GetComponent<Rigidbody>())
            pieces[1].AddComponent<Rigidbody>();

        pieces[0].GetComponent<Rigidbody>().isKinematic = false;
        var forceZero = pieces[0].transform.position - transform.position;
        forceZero = forceZero.normalized;
        pieces[0].GetComponent<Rigidbody>().AddForce(-normal.normalized * forceMultiplier * force);

        var forceOne = pieces[1].transform.position - transform.position;
        forceOne = forceZero.normalized;
        pieces[1].GetComponent<Rigidbody>().AddForce(normal.normalized.normalized * forceMultiplier*force);
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
