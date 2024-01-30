using UnityEngine;

public class BehaviourOnOffExample : MonoBehaviour
{
    // �_�ł�����Ώہi������Behaviour�ɕύX����Ă���j
    [SerializeField] private Behaviour _target;
    // �_�Ŏ���[s]
    [SerializeField] private float Cycle = 1;

    private double Time;

    private void Update()
    {
        // �����������o�߂�����
        Time += Time.deltaTime;

        // ����cycle�ŌJ��Ԃ��l�̎擾
        // 0�`cycle�͈̔͂̒l��������
        var repeatValue = Mathf.Repeat((float)Time, Cycle);

        // ��������time�ɂ����閾�ŏ�Ԃ𔽉f
        _target.enabled = repeatValue >= Cycle * 0.5f;
    }
}