using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SowManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject SnowEffect;
    private GameObject Player;
    private GameObject NowSnowEffect;
    void Start()
    {
        Player = GameObject.Find("Player");
        transform.position = Player.transform.position + new Vector3(0f, 700f, 0f);
        NowSnowEffect = Instantiate(SnowEffect, transform.position, SnowEffect.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + new Vector3(0f, 700f, 0f);
    }

    private void OnTriggerExit(Collider other)
    {
        SowEffectManager snow;
        snow = NowSnowEffect.GetComponent<SowEffectManager>();
        snow.flg = true;
        NowSnowEffect = Instantiate(SnowEffect, transform.position, SnowEffect.transform.rotation);

    }
}
