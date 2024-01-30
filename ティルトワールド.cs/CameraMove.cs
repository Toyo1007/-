using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CameraMove : MonoBehaviour
{
    [SerializeField] Vector3 OffSet;
    [SerializeField] float PlayerSize;
    [SerializeField] float rotateSpeed;
    float Olddifference;
    Vector3 PlayeOldPos;
    //public float angleH;
    public float angleV;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        OffSet = new Vector3(0f, 621f, -480f);

        Player = GameObject.Find("Player");
        PlayerSize = Player.transform.localScale.magnitude;
        PlayeOldPos = Player.transform.position;
        transform.position = PlayeOldPos + OffSet;
        transform.rotation = Quaternion.Euler(new Vector3(50f, 0f, 0f));
        Olddifference = OffSet.magnitude;
        rotateSpeed = 0.3f;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //angleH = Input.GetAxis("Horizontal2") * rotateSpeed;
        //angleV = Input.GetAxis("Vertical2") * rotateSpeed;
        //プレイヤー位置情報
        Vector3 playerPos = Player.transform.position;
        //カメラの追従
        transform.position += playerPos - PlayeOldPos;
        PlayeOldPos = playerPos;
        //カメラを回転させる
        //transform.RotateAround(playerPos, Vector3.up, angleH);
        //transform.RotateAround(playerPos, transform.right, angleV);
        //プレイヤーの大きさでカメラのoffsetを変える
        if (Player.transform.localScale.magnitude / PlayerSize >= 0.8f)
        {
            Vector3 difference = transform.position - playerPos;
            difference.Normalize();
            transform.position += difference * OffSet.magnitude * (Player.transform.localScale.magnitude / PlayerSize) - difference * Olddifference;

            Olddifference = OffSet.magnitude * (Player.transform.localScale.magnitude / PlayerSize);
        }
    }

}
