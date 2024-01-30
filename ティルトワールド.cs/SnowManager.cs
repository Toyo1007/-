using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成するGameObject")]
    private GameObject createPrefab;
    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Vector3 rangeA = new Vector3(1000f,1000f, 1000f);
    [SerializeField]
    [Tooltip("生成する範囲B")]
    private Vector3 rangeB = new Vector3(-1000f, 900f, -1000f);

    // 経過時間
    private float time;

    // Update is called once per frame
    void Update()
    {
        // 前フレームからの時間を加算していく
        time = time + Time.deltaTime;

        // 約1秒置きにランダムに生成されるようにする。
        if (time > 0.03f)
        {
            Vector3 A = rangeA + transform.position;
            Vector3 B = rangeB + transform.position;
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            float x = Random.Range(A.x, B.x);
            // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
            float y = Random.Range(A.y, B.y);
            // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
            float z = Random.Range(A.z, B.z);

            // GameObjectを上記で決まったランダムな場所に生成
            Instantiate(createPrefab, new Vector3(x, y, z), createPrefab.transform.rotation);

            // 経過時間リセット
            time = 0f;
        }
    }
}
