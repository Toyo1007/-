using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObjZ : MonoBehaviour
{
    [SerializeField] private float Rot;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
    rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = transform.forward * Rot;
    }
}
