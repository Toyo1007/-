using System.Collections;
using System.Collections.Generic;
//using UnityEditor.AnimatedValues;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] AudioClip clip;
    private float MaxAngularSpeed = Mathf.Infinity;
    public GameObject Obj;
    private GameObject CreateObj;
    private float SpeedFactor;
    private float SmoothTime = 0.1f;
    private float CurrentAngularVelocity;
    private Vector3 RotateAxis;

    private Vector3 SetPosition;
    private bool GoalFlag = false;
    private float Len;
    private bool AnimFlag = false;
    private bool AnimFlag2 = false;
    private bool AnimFlag3 = false;
    public bool AnimEndFlag = false;
    private float RotSpeedFairy = 0.4f;
    private float Frame = 0.0f;

    Vector3 CreateObjPos;
    Vector3 PlayerPos;
    Vector3 OldCreateObjPos;




    // Start is called before the first frame update
    void Start()
    {
        RotateAxis = Vector3.up;
        SpeedFactor = 0.1f;
        PlayerPos = this.GetComponent<Transform>().position;

        PlayerPos.x += (this.GetComponent<Transform>().localScale.x / 2);
        PlayerPos.z -= (this.GetComponent<Transform>().localScale.z / 2);

        CreateObj = Instantiate(Obj, new Vector3(PlayerPos.x, PlayerPos.y, PlayerPos.z), Quaternion.identity);
        CreateObjPos = CreateObj.GetComponentInParent<Transform>().position;
        OldCreateObjPos = CreateObjPos;

        AnimEndFlag = false;
        source = CreateObj.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        OldCreateObjPos = CreateObjPos;
        CreateObjPos = CreateObj.transform.position;

        if (GoalFlag == false)
        {
            //// 指定オブジェクトと自身の現在位置を取得する
            Vector3 selfPosition = CreateObj.transform.position;
            Vector3 targetPosition = this.transform.position;


            // 現在の距離と半径距離の差分を取得する
            float diffDistance = Vector3.Distance(selfPosition, targetPosition) - (this.GetComponent<Transform>().localScale.z / 2 + 40);

            // 指定半径の距離になるよう近づく(or離れる)
            CreateObj.transform.position = Vector3.MoveTowards(selfPosition, targetPosition, diffDistance);

            // 指定オブジェクトを中心に回転する
            CreateObj.transform.RotateAround(
                targetPosition,
                RotateAxis,
                360.0f / (1.0f / SpeedFactor) * Time.deltaTime
                );

            Vector3 Dir = CreateObjPos - OldCreateObjPos;

            if (Dir == Vector3.zero)
                return;

            var targetRot = Quaternion.LookRotation(Dir, Vector3.up);

            // 現在の向きと進行方向との角度差を計算
            var diffAngle = Vector3.Angle(CreateObj.transform.forward, Dir);
            // 現在フレームで回転する角度の計算
            var rotAngle = Mathf.SmoothDampAngle(
                0,
                diffAngle,
                ref CurrentAngularVelocity,
                SmoothTime,
                MaxAngularSpeed
            );
            // 現在フレームにおける回転を計算
            var nextRot = Quaternion.RotateTowards(
                CreateObj.transform.rotation,
                targetRot,
                rotAngle
            );

            // オブジェクトの回転に反映
            CreateObj.transform.rotation = nextRot;
        }

    }

    //ゴールしたら
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Goal"))
        {
            if (GoalFlag == false)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(clip);
                }
                // 衝突位置を取得する
                Vector3 hitPos = collision.contacts[0].point;

                // 衝突位置から自機へ向かうベクトルを求める
                Vector3 boundVec = this.transform.position - hitPos;

                // 衝突した面の法線ベクトルを取得する
                Vector3 collisionNormal = collision.contacts[0].normal;

                // 上方向（Y軸方向）との内積を計算し、衝突が上方向からのものか判定する
                float dotProduct = Vector3.Dot(collisionNormal, Vector3.up);

                if (dotProduct > 0.5f) // 上方向からの衝突の判定条件を適宜調整してください
                {
                    if (transform.localScale.z >= 150)
                    {
                        CreateObj.transform.position = collision.transform.position + new Vector3(0.0f, 0.0f, transform.localScale.z);
                    }
                    else
                    {
                        CreateObj.transform.position = collision.transform.position + new Vector3(0.0f, 0.0f, collision.transform.localScale.z);
                    }
                }
                else
                {
                    if (transform.localScale.z >= 150)
                    {
                        CreateObj.transform.position = collision.transform.position + (-boundVec.normalized * transform.localScale.z);
                    }
                    else
                    {
                        CreateObj.transform.position = collision.transform.position + (-boundVec.normalized * collision.transform.localScale.z);
                    }
                }

            }

            GoalFlag = true;
            Len = (SetPosition - CreateObj.transform.position).magnitude;

            if (Len != 0 && AnimFlag == false)
            {
                SetPosition = transform.position + Vector3.up * (transform.localScale.y + 100);

                RotSpeedFairy = 0.4f * (transform.localScale.z / 100);

                if (RotSpeedFairy > 0.4)
                {
                    RotSpeedFairy = 0.4f;
                }

                CreateObj.transform.position = Vector3.MoveTowards(CreateObj.transform.position, SetPosition, RotSpeedFairy * 400 * Time.deltaTime);

                CreateObj.transform.RotateAround(
                transform.position,
                RotateAxis,
                360.0f / (1.0f / 1) * Time.deltaTime
                );

                Vector3 Dir = CreateObjPos - OldCreateObjPos;

                if (Dir == Vector3.zero)
                    return;

                var targetRot = Quaternion.LookRotation(Dir, Vector3.up);

                // 現在の向きと進行方向との角度差を計算
                var diffAngle = Vector3.Angle(CreateObj.transform.forward, Dir);
                // 現在フレームで回転する角度の計算
                var rotAngle = Mathf.SmoothDampAngle(
                    0,
                    diffAngle,
                    ref CurrentAngularVelocity,
                    SmoothTime,
                    MaxAngularSpeed
                );
                // 現在フレームにおける回転を計算
                var nextRot = Quaternion.RotateTowards(
                    CreateObj.transform.rotation,
                    targetRot,
                    rotAngle
                );

                // オブジェクトの回転に反映
                CreateObj.transform.rotation = nextRot;
            }
            else
            {
                AnimFlag = true;
                Len = (new Vector3(SetPosition.x, SetPosition.y, SetPosition.z + 100) - CreateObj.transform.position).magnitude;
                if (Len > 1 && AnimFlag2 == false)
                {

                    CreateObj.transform.position = Vector3.MoveTowards(CreateObj.transform.position, new Vector3(SetPosition.x, SetPosition.y, SetPosition.z + 100), 0.9f * 300 * Time.deltaTime);
                }
                else
                {
                    AnimFlag2 = true;

                    if (Frame < 1200)
                    {
                        CreateObj.transform.RotateAround(
                       SetPosition,
                       -Vector3.right,
                       360.0f / (1.0f / 0.5f) * Time.deltaTime
                       );
                        Frame++;
                    }
                    else
                    {

                        AnimFlag3 = true;
                        CreateObj.transform.position = Vector3.MoveTowards(CreateObj.transform.position, collision.transform.position, 0.7f * 300 * Time.deltaTime);

                        //Debug.Log(Vector3.Distance(CreateObj.transform.position, collision.transform.position));
                        if (Vector3.Distance(CreateObj.transform.position, collision.transform.position) < 1.0f)
                        {
                            AnimEndFlag = true;
                        }
                    }
                }


                Vector3 Dir = CreateObjPos - OldCreateObjPos;

                if (Dir == Vector3.zero)
                    return;

                var targetRot = Quaternion.LookRotation(Dir, -Vector3.up);

                // 現在の向きと進行方向との角度差を計算
                var diffAngle = Vector3.Angle(CreateObj.transform.forward, Dir);
                // 現在フレームで回転する角度の計算
                var rotAngle = Mathf.SmoothDampAngle(
                    0,
                    diffAngle,
                    ref CurrentAngularVelocity,
                    SmoothTime,
                    MaxAngularSpeed
                );
                // 現在フレームにおける回転を計算
                var nextRot = Quaternion.RotateTowards(
                    CreateObj.transform.rotation,
                    targetRot,
                    rotAngle
                );

                // オブジェクトの回転に反映
                CreateObj.transform.rotation = nextRot;

            }



        }

    }
}