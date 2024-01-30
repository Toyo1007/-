using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    public GameObject RightCube;
    public GameObject LeftCube;
    public Rigidbody rb;
    private float speedR;
    private float speedL;
    private bool HitR;
    private bool HitL;
    void Start()
    {
        parent = transform.parent.gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
        speedR = 0f;
        speedL = 0f;
        HitR = false;
        HitL = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(RightCube.transform.position.y < LeftCube.transform.position.y && !HitR)
        {
            rb.velocity = transform.right * speedR;
            speedR += 5f;
        }
        else
        {
            speedR = 0f;
        }

        if (RightCube.transform.position.y > LeftCube.transform.position.y && !HitL)
        {
            rb.velocity = transform.right * speedL;
            speedL -= 5f;

        }
        else
        {
            speedL = 0f;
        }
        HitR = false;
        HitL = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == "CubeRight")
        {
            HitR = true;
        }
        if (collision.gameObject.name == "CubeLeft")
        {
            HitL = true;
        }
    }
}
