using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySwitch : MonoBehaviour
{
    public Switch SwitchData;
    public List<GameObject> Pole = new List<GameObject>();
    float Count;
    int PoleFlag;

    // Start is called before the first frame update
    void Start()
    {
        Count = 0.0f;
        PoleFlag = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if (SwitchData.SwitchFlag == true)
        {
            if (PoleFlag > 3)
                return;

            if (Count > 1f)
            {
                switch (PoleFlag)
                {
                    case 0:
                        Pole[0].SetActive(true);
                        Pole[1].SetActive(true);
                        break;
                    case 1:
                        Pole[2].SetActive(true);
                        Pole[3].SetActive(true);
                        break;
                    case 2:
                        Pole[4].SetActive(true);
                        Pole[5].SetActive(true);
                        break;
                    case 3:
                        Pole[6].SetActive(true);
                        Pole[7].SetActive(true);
                        break;
                }
                Count = 0f;
                PoleFlag++;
            }
            Count += Time.deltaTime;

        }
        else
        {
            for (int i = 0; i < Pole.Count; i++)
            {
                Pole[i].SetActive(false);
            }
        }
    }
}
