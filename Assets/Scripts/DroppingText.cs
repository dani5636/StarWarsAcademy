using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
 
public class DroppingText : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    private bool isDebugging = false;

    [Header("Settings")]
    [SerializeField]
    private float dropSpeed = 10;

    [SerializeField]
    private float timer = 75f;

    [SerializeField]
    private float stopYPosition = -220.0f;

    private Vector3 localVectorDown;


    void Update()
    {
        MoveDown();

    }

    private void MoveDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && CanMove())
        {
            Vector3 pos = transform.position;
            localVectorDown = transform.TransformDirection(0, -1, -0.1f);
            pos += localVectorDown * dropSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }

    private bool CanMove(){
        if(isDebugging){
        Debug.Log("Y: " + GetComponent<RectTransform>().localPosition.y);
        }
        if(GetComponent<RectTransform>().localPosition.y <= stopYPosition )
        {
            return false;
        }
        return true;
        }


}
