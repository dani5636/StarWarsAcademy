using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : MonoBehaviour
{
    //Test travelTime
    [SerializeField]
    public float speed = -4.0f;
    public bool isDebugging = false;

    private float travelledTime;
    [HideInInspector]
    public NodeManager _manager;
    [HideInInspector]
    public bool hitable;

    void Start()
    {
        hitable = false;
    }
    // Update is called once per frame
    void Update()
    {
        travelledTime += Time.deltaTime;
        transform.Translate((Vector3.forward * speed) * Time.deltaTime);
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trigger")
        {
            hitable = true;
        }
        if (other.tag == "KillNodes") {
            Destroy(gameObject);
        }
    }

    public void OnHit(Vector3 directionOfTravel)
    {
        if(isDebugging)
        _manager.NodeHit();

    }
}
