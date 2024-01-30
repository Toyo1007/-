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
        // transform���擾
        Transform myTransform = this.transform;

        // ���[�J�����W�ł̍��W���擾
        Vector3 localPos = myTransform.localPosition;
        localPos.x += 0.0001f;    // ���[�J�����W����ɂ����Ax���W��1�ɕύX
        myTransform.localPosition = localPos; // ���[�J�����W�ł̍��W��ݒ�

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
