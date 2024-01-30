using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YRot : MonoBehaviour
{
    // Start is called before the first frame update
    public float angleH;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 1f))
        {
            float angleH = Input.GetAxis("Horizontal2");
            Vector3 rot = new Vector3(0f, 150f, 0f) * Time.deltaTime;


            if (Input.GetKey(KeyCode.LeftArrow)|| angleH < 0f)
            {
                transform.Rotate(rot, Space.World);

            }
            if (Input.GetKey(KeyCode.RightArrow) || angleH > 0f)
            {
                transform.Rotate(-rot, Space.World);

            }
        }
    }
}
