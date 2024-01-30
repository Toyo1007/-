using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transformを取得
        Transform myTransform = this.transform;

        // ローカル座標での座標を取得
        Vector3 localPos = myTransform.localPosition;
        localPos.x += 0.0001f;    // ローカル座標を基準にした、x座標を1に変更
        myTransform.localPosition = localPos; // ローカル座標での座標を設定

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
