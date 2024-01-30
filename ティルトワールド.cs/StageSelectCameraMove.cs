using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectCameraMove : MonoBehaviour
{
    private GameObject player;
    private operation_Fairy of;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        of = GameObject.FindGameObjectWithTag("Player").GetComponent<operation_Fairy>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(of.MovePos_Fairy.x,
                                         of.MovePos_Fairy.y,
                                         transform.position.z);
    }
}
