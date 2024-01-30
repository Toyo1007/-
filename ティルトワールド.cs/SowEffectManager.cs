using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SowEffectManager : MonoBehaviour
{
    public bool flg = false;
    // Start is called before the first frame update
    void Start()
    {
        flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flg)
        {
            Destroy(gameObject, 10f);


        }
    }
}
