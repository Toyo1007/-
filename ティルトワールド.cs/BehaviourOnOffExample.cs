using UnityEngine;

public class BehaviourOnOffExample : MonoBehaviour
{
    // 点滅させる対象（ここがBehaviourに変更されている）
    [SerializeField] private Behaviour _target;
    // 点滅周期[s]
    [SerializeField] private float Cycle = 1;

    private double Time;

    private void Update()
    {
        // 内部時刻を経過させる
        Time += Time.deltaTime;

        // 周期cycleで繰り返す値の取得
        // 0～cycleの範囲の値が得られる
        var repeatValue = Mathf.Repeat((float)Time, Cycle);

        // 内部時刻timeにおける明滅状態を反映
        _target.enabled = repeatValue >= Cycle * 0.5f;
    }
}