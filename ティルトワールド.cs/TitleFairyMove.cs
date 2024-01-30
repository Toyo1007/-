using System.Collections;
using System.Collections.Generic;
//using UnityEditor.AnimatedValues;
using UnityEngine;

public class TitleFairyMove : MonoBehaviour
{
    private float _maxAngularSpeed = Mathf.Infinity;
    public GameObject Fairy;
    private float SpeedFactor;
    private float SmoothTime = 0.1f;
    private float CurrentAngularVelocity;
    private Vector3 RotateAxis;

   
    Vector3 CreateObjPos;
    Vector3 OldCreateObjPos;


    // Start is called before the first frame update
    void Start()
    {
        RotateAxis = Vector3.up;
        SpeedFactor = 0.3f;
        OldCreateObjPos = CreateObjPos;

    }

    // Update is called once per frame
    void Update()
    {
        OldCreateObjPos = CreateObjPos;
        CreateObjPos = Fairy.transform.position;

        //// 指定オブジェクトと自身の現在位置を取得する
        Vector3 selfPosition = Fairy.transform.position;
        Vector3 targetPosition = this.transform.position;


        // 現在の距離と半径距離の差分を取得する
        float diffDistance = Vector3.Distance(selfPosition, targetPosition) - 400;

        // 指定半径の距離になるよう近づく(or離れる)
        Fairy.transform.position = Vector3.MoveTowards(selfPosition, targetPosition, diffDistance);

        // 指定オブジェクトを中心に回転する
        Fairy.transform.RotateAround(
            targetPosition,
            RotateAxis,
            360.0f / (1.0f / SpeedFactor) * Time.deltaTime
            );

        Vector3 Dir = CreateObjPos - OldCreateObjPos;

        if (Dir == Vector3.zero)
            return;

        var targetRot = Quaternion.LookRotation(Dir, Vector3.up);

        // 現在の向きと進行方向との角度差を計算
        var diffAngle = Vector3.Angle(Fairy.transform.forward, Dir);
        // 現在フレームで回転する角度の計算
        var rotAngle = Mathf.SmoothDampAngle(
            0,
            diffAngle,
            ref CurrentAngularVelocity,
            SmoothTime,
            _maxAngularSpeed
        );
        // 現在フレームにおける回転を計算
        var nextRot = Quaternion.RotateTowards(
            Fairy.transform.rotation,
            targetRot,
            rotAngle
        );

        // オブジェクトの回転に反映
        Fairy.transform.rotation = nextRot;
    }


}
