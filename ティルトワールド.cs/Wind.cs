using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    public GameObject Player;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player.GetComponent<Rigidbody>().AddForce(this.transform.right * -500.0f);
            
        }
    }
}
