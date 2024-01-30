using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFairy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float upDownLen = 10f;
    [SerializeField]
    float upDownSpeed = 0.05f;
    private float g_rot = 0f;
    private Transform player;
    [SerializeField]
    private bool LookMy = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        g_rot+= upDownSpeed * Time.deltaTime;
        transform.position += transform.up * Mathf.Sin(g_rot) * upDownLen;

        if (LookMy)
        {
            float rot = Vector3.SignedAngle(transform.forward, new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position, Vector3.up);
            transform.RotateAround(transform.position, transform.up, rot * Time.deltaTime);
        }
    }

}
