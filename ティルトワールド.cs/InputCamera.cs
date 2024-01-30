using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public float angle;
    [SerializeField]private float rotSpeed;
    void Start()
    {
        angle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        angle = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle -= rotSpeed;
            

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle += rotSpeed;
        }

    }
}
