using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjSwitch : MonoBehaviour
{

    public Switch SwitchData;
    [SerializeField] private List<GameObject> CreateFloor;
    public List<GameObject> StartPos;
    public GameObject StageCore;
    bool MoveFlag;


    private void Start()
    {
        for (int i = 0; i < CreateFloor.Count; i++)
        {
            MoveFlag = false;
            StartPos[i].transform.position = CreateFloor[i].transform.position;
            CreateFloor[i].transform.position += Vector3.up * 4000f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (SwitchData.SwitchFlag == true)
        {

            for (int i = 0; i < CreateFloor.Count; i++)
            { 
                if (Vector3.Distance(CreateFloor[i].transform.position, StartPos[i].transform.position) > 10.0f)
                {
                    Debug.Log(Vector3.Distance(CreateFloor[i].transform.position, StartPos[i].transform.position));
                    if (MoveFlag == false)
                        CreateFloor[i].transform.position = Vector3.MoveTowards(CreateFloor[i].transform.position, StartPos[i].transform.position, 1000.0f * Time.deltaTime);
                }
                else
                {
                    MoveFlag = true;
                }

            }

        }

    }
}
