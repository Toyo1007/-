using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��������GameObject")]
    private GameObject createPrefab;
    [SerializeField]
    [Tooltip("��������͈�A")]
    private Vector3 rangeA = new Vector3(1000f,1000f, 1000f);
    [SerializeField]
    [Tooltip("��������͈�B")]
    private Vector3 rangeB = new Vector3(-1000f, 900f, -1000f);

    // �o�ߎ���
    private float time;

    // Update is called once per frame
    void Update()
    {
        // �O�t���[������̎��Ԃ����Z���Ă���
        time = time + Time.deltaTime;

        // ��1�b�u���Ƀ����_���ɐ��������悤�ɂ���B
        if (time > 0.03f)
        {
            Vector3 A = rangeA + transform.position;
            Vector3 B = rangeB + transform.position;
            // rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float x = Random.Range(A.x, B.x);
            // rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float y = Random.Range(A.y, B.y);
            // rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float z = Random.Range(A.z, B.z);

            // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
            Instantiate(createPrefab, new Vector3(x, y, z), createPrefab.transform.rotation);

            // �o�ߎ��ԃ��Z�b�g
            time = 0f;
        }
    }
}
