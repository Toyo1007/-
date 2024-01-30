using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchErase : MonoBehaviour
{
    public Switch SwitchData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(SwitchData.SwitchFlag == true)
        {
            Destroy(this.gameObject);
        }
    }
}
