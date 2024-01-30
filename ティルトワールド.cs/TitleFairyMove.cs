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

        //// �w��I�u�W�F�N�g�Ǝ��g�̌��݈ʒu���擾����
        Vector3 selfPosition = Fairy.transform.position;
        Vector3 targetPosition = this.transform.position;


        // ���݂̋����Ɣ��a�����̍������擾����
        float diffDistance = Vector3.Distance(selfPosition, targetPosition) - 400;

        // �w�蔼�a�̋����ɂȂ�悤�߂Â�(or�����)
        Fairy.transform.position = Vector3.MoveTowards(selfPosition, targetPosition, diffDistance);

        // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
        Fairy.transform.RotateAround(
            targetPosition,
            RotateAxis,
            360.0f / (1.0f / SpeedFactor) * Time.deltaTime
            );

        Vector3 Dir = CreateObjPos - OldCreateObjPos;

        if (Dir == Vector3.zero)
            return;

        var targetRot = Quaternion.LookRotation(Dir, Vector3.up);

        // ���݂̌����Ɛi�s�����Ƃ̊p�x�����v�Z
        var diffAngle = Vector3.Angle(Fairy.transform.forward, Dir);
        // ���݃t���[���ŉ�]����p�x�̌v�Z
        var rotAngle = Mathf.SmoothDampAngle(
            0,
            diffAngle,
            ref CurrentAngularVelocity,
            SmoothTime,
            _maxAngularSpeed
        );
        // ���݃t���[���ɂ������]���v�Z
        var nextRot = Quaternion.RotateTowards(
            Fairy.transform.rotation,
            targetRot,
            rotAngle
        );

        // �I�u�W�F�N�g�̉�]�ɔ��f
        Fairy.transform.rotation = nextRot;
    }


}
