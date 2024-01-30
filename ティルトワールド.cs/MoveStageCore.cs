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

        //�e���ړ����q�I�u�W�F�N�g�������Ȃ��悤�ɏC������
        if (transform.position != Player.transform.position)
        {
            Vector3 difference = transform.position - Player.transform.position;
            transform.position = Player.transform.position;
            foreach (Transform childTransform in transform)
            {
                //�S�Ă̎q�I�u�W�F�N�g�̍��W���ړ���������
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
    Vector3 axisNormalized, // ���x�N�g���͗v���K��
    float theta)
    {
        // v�����Ɏˉe���āA��]�~���Sc�𓾂�
        var c = ProjectVector(v, axisNormalized);
        var p = v - c;

        // p�y��a�ƒ�������x�N�^�𓾂�
        var q = Vector3.Cross(axisNormalized, p);
        // a,p�͒������Ă��āA|a|=1�Ȃ̂ŁA|q|=|p|

        // ��]���v'�̏I�_V'�́AV' = C + s*p + t*q �ƕ\����B
        // �����ŁAs=cos��,  t=sin��
        var s = Mathf.Cos(theta);
        var t = Mathf.Sin(theta);
        return c + (p * s) + (q * t);
    }
}
