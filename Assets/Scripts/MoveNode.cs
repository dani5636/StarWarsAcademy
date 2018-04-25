using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : MonoBehaviour
{
    //Test travelTime
    [SerializeField]
    public float speed = -4.0f;
    // Use this for initialization
    private float travelledTime;
    public NodeManager _manager;
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
        if (other.tag == "EndWall") {
            Destroy(gameObject);
        }
    }

    public void OnHit()
    {
        _manager.NodeHit();
    }
}
