using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform Destination;//移動先
    private Transform newDestination;//現在の移動先
    public float moveSpeed;//移動速度
    private float maxspeed;
    GameObject statePos;//開始位置
    Rigidbody rb;
    private bool hitObj;
    private int stopCounter;
    private bool stopflg;
    void Start()
    {
        statePos = new GameObject();
        statePos.transform.parent = transform.root.gameObject.transform;
        statePos.transform.position = this.transform.position;
        statePos.tag = "PointB";
        statePos.AddComponent<SphereCollider>().isTrigger = true;
        newDestination = Destination;
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3 movevec = newDestination.position - transform.position;
        maxspeed = moveSpeed;
        hitObj = false;
        stopCounter = 0;
        stopflg = true;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!stopflg)
        {
            Vector3 movevec = newDestination.position - transform.position;
            rb.velocity = movevec.normalized * moveSpeed;

        }
        else
        {
            stopCounter++;
            if(stopCounter >= 1000)
            {
                Debug.Log("stop");
                stopflg = false;
                stopCounter = 0;

            }
        }

        hitObj = false;
    }
    private void OnTriggerStay(Collider other)
    {
        Vector3 len = transform.position - other.transform.position;

        if (other.transform == Destination && newDestination.tag =="PointA")
        {
            if (len.magnitude < 10f) 
            {
                transform.position = other.transform.position;
                newDestination = statePos.transform;
                stopflg = true;
            }
            hitObj = true;
            rb.velocity = Vector3.zero;
        }
        if (other.transform == statePos.transform && newDestination.tag == "PointB")
        {
            if (len.magnitude < 10f)
            {
                transform.position = other.transform.position;
                newDestination = Destination;

                stopflg = true;
                rb.velocity = Vector3.zero;
            }
            hitObj = true;
        }
    }
}
