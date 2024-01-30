using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class JumpSonwMan : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float BasePower = 3f;
    private float jumpPower;
    private float uplen = 0f;
    private GameObject statePos;//ŠJŽnˆÊ’u
    private float count = 0f;
    private bool jumpFlg = false;
    [SerializeField]
    private float countTime = 1000;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        jumpPower = BasePower;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (jumpFlg)
            {
                jumpPower -= 0.03f;
                uplen += jumpPower;
                if (uplen < 0)
                {
                    transform.position += transform.up * -uplen * Time.deltaTime * 150f;
                    uplen = 0f;
                    jumpPower = BasePower;
                    jumpFlg = false;
                    count = Time.deltaTime;
                }
                else
                {
                    transform.position += transform.up * jumpPower;
                }
            }
            else
            {
                count++;
                if (count > countTime)
                {
                    jumpFlg = true;
                }
            }
            float rot = Vector3.SignedAngle(transform.forward, new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position, Vector3.up);

            transform.RotateAround(transform.position, transform.up, rot * Time.deltaTime);
        }
    }

}
