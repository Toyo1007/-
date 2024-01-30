using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingFloor : MonoBehaviour
{
    [SerializeField] private List<GameObject> RedFloor;
    [SerializeField] private List<GameObject> BlueFloor;
    [SerializeField] private float ChangeCounter;


    private bool ChangeFlag;
    private float Counter;
 
    // Start is called before the first frame update
    void Start()
    {
        ChangeFlag = false;
        Counter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ChangeFlag);
        if (Counter > ChangeCounter)
        {
            ChangeFlag = !ChangeFlag;
            Counter = 0;
        }
        Counter++;

        foreach (GameObject Red in RedFloor)
        {
            if (ChangeFlag == true)
            {
                Red.SetActive(false);
            }
            else
            {
                Red.SetActive(true);
            }
        }
        foreach (GameObject Blue in BlueFloor)
        {
            if (ChangeFlag == true)
            {
                Blue.SetActive(true);
            }
            else
            {
                Blue.SetActive(false);
            }
        }
    }
}
