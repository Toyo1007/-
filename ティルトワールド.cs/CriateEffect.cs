using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CriateEffect : MonoBehaviour
{
 
    [SerializeField]
    private GameObject CroateObj;
    private Rigidbody rb;
    private float BaseSecelPlayer;
    private int timer;
    [SerializeField]
    [Header("エフェクト生成のポジションを前にずらす")]
    private float fasutopos;
    // Start is called before the first frame update
    void Start()
    {//エフェクトの生成
        //プレイヤー情報を取得
        rb = GetComponent<Rigidbody>();
        BaseSecelPlayer = transform.localScale.magnitude;
        fasutopos = 0.3f;
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude > 300)
        {
            timer++;
            if (timer > 600)
            {
                timer = 0;
                //エフェクトの回転
                var diffAngle = Vector3.Angle(-CroateObj.transform.forward, rb.velocity);
                Vector3 Nomal = Vector3.Cross(-CroateObj.transform.forward, rb.velocity).normalized;
                CroateObj.transform.RotateAround(CroateObj.transform.position, Nomal, diffAngle);
                GameObject parent = Instantiate(CroateObj, transform.position + rb.velocity * fasutopos - Vector3.up * transform.localScale.y / 3, Quaternion.identity);
                parent.transform.parent = GameObject.Find(" StageCore").transform;
                float ratio = transform.localScale.magnitude / BaseSecelPlayer;
                foreach (Transform child in parent.transform)
                {
                    // 子オブジェクトのサイズ調整
                    child.localScale *= ratio;
                }
                CroateObj.transform.rotation = Quaternion.identity;
                CroateObj.transform.localScale /= ratio;

            }
        }
        else
        {
            timer = 0;
        }
    }
}
