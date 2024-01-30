using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStageCore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    public bool moveflog;
    GameObject Player;
    GameObject Camera;
    public CameraMove cameraMove;

    private float rotstage;
    void Start()
    {
        Player = GameObject.Find("Player");
        Camera = GameObject.Find("Main Camera");
        speed = 0.3f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        //親が移動時子オブジェクトが動かないように修正する
        if (transform.position != Player.transform.position)
        {
            Vector3 difference = transform.position - Player.transform.position;
            transform.position = Player.transform.position;
            foreach (Transform childTransform in transform)
            {
                //全ての子オブジェクトの座標を移動分下げる
                childTransform.position += difference;
            }
        }
        float x = Input.GetAxis("Vertical") * speed;
        float z = -Input.GetAxis("Horizontal") * speed;

        Vector3 zrot = Camera.transform.forward;
        zrot.y = 0f;
        transform.RotateAround(transform.position, Camera.transform.right, x);
        transform.RotateAround(transform.position, zrot, z);
    }

    static private Vector3 ProjectVector(
Vector3 v,
Vector3 axisNormalized)
    {
        return Vector3.Dot(v, axisNormalized) * axisNormalized;
    }

    static private Vector3 RotateVector(
    Vector3 v,
    Vector3 axisNormalized, // 軸ベクトルは要正規化
    float theta)
    {
        // vを軸に射影して、回転円中心cを得る
        var c = ProjectVector(v, axisNormalized);
        var p = v - c;

        // p及びaと直交するベクタを得る
        var q = Vector3.Cross(axisNormalized, p);
        // a,pは直交していて、|a|=1なので、|q|=|p|

        // 回転後のv'の終点V'は、V' = C + s*p + t*q と表せる。
        // ここで、s=cosθ,  t=sinθ
        var s = Mathf.Cos(theta);
        var t = Mathf.Sin(theta);
        return c + (p * s) + (q * t);
    }
}
