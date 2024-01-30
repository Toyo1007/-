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
            //// �w��I�u�W�F�N�g�Ǝ��g�̌��݈ʒu���擾����
            Vector3 selfPosition = CreateObj.transform.position;
            Vector3 targetPosition = this.transform.position;


            // ���݂̋����Ɣ��a�����̍������擾����
            float diffDistance = Vector3.Distance(selfPosition, targetPosition) - (this.GetComponent<Transform>().localScale.z / 2 + 40);

            // �w�蔼�a�̋����ɂȂ�悤�߂Â�(or�����)
            CreateObj.transform.position = Vector3.MoveTowards(selfPosition, targetPosition, diffDistance);

            // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
            CreateObj.transform.RotateAround(
                targetPosition,
                RotateAxis,
                360.0f / (1.0f / SpeedFactor) * Time.deltaTime
                );

            Vector3 Dir = CreateObjPos - OldCreateObjPos;

            if (Dir == Vector3.zero)
                return;

            var targetRot = Quaternion.LookRotation(Dir, Vector3.up);

            // ���݂̌����Ɛi�s�����Ƃ̊p�x�����v�Z
            var diffAngle = Vector3.Angle(CreateObj.transform.forward, Dir);
            // ���݃t���[���ŉ�]����p�x�̌v�Z
            var rotAngle = Mathf.SmoothDampAngle(
                0,
                diffAngle,
                ref CurrentAngularVelocity,
                SmoothTime,
                MaxAngularSpeed
            );
            // ���݃t���[���ɂ������]���v�Z
            var nextRot = Quaternion.RotateTowards(
                CreateObj.transform.rotation,
                targetRot,
                rotAngle
            );

            // �I�u�W�F�N�g�̉�]�ɔ��f
            CreateObj.transform.rotation = nextRot;
        }

    }

    //�S�[��������
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
                // �Փˈʒu���擾����
                Vector3 hitPos = collision.contacts[0].point;

                // �Փˈʒu���玩�@�֌������x�N�g�������߂�
                Vector3 boundVec = this.transform.position - hitPos;

                // �Փ˂����ʂ̖@���x�N�g�����擾����
                Vector3 collisionNormal = collision.contacts[0].normal;

                // ������iY�������j�Ƃ̓��ς��v�Z���A�Փ˂����������̂��̂����肷��
                float dotProduct = Vector3.Dot(collisionNormal, Vector3.up);

                if (dotProduct > 0.5f) // ���������̏Փ˂̔��������K�X�������Ă�������
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

                // ���݂̌����Ɛi�s�����Ƃ̊p�x�����v�Z
                var diffAngle = Vector3.Angle(CreateObj.transform.forward, Dir);
                // ���݃t���[���ŉ�]����p�x�̌v�Z
                var rotAngle = Mathf.SmoothDampAngle(
                    0,
                    diffAngle,
                    ref CurrentAngularVelocity,
                    SmoothTime,
                    MaxAngularSpeed
                );
                // ���݃t���[���ɂ������]���v�Z
                var nextRot = Quaternion.RotateTowards(
                    CreateObj.transform.rotation,
                    targetRot,
                    rotAngle
                );

                // �I�u�W�F�N�g�̉�]�ɔ��f
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

                // ���݂̌����Ɛi�s�����Ƃ̊p�x�����v�Z
                var diffAngle = Vector3.Angle(CreateObj.transform.forward, Dir);
                // ���݃t���[���ŉ�]����p�x�̌v�Z
                var rotAngle = Mathf.SmoothDampAngle(
                    0,
                    diffAngle,
                    ref CurrentAngularVelocity,
                    SmoothTime,
                    MaxAngularSpeed
                );
                // ���݃t���[���ɂ������]���v�Z
                var nextRot = Quaternion.RotateTowards(
                    CreateObj.transform.rotation,
                    targetRot,
                    rotAngle
                );

                // �I�u�W�F�N�g�̉�]�ɔ��f
                CreateObj.transform.rotation = nextRot;

            }



        }

    }
}