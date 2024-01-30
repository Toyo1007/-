using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAfter_FairyMove : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject MainCamera;
    private float rot = 0f;
    public Vector3 SF_Base_pos;
    private bool flg = true;
    private Vector3 OldPos;
    private float _smoothTime = 0.1f;
    private float _currentAngularVelocity;
    private float _maxAngularSpeed = Mathf.Infinity;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    void OnEnable()
    {
        if (flg)
        {
            this.enabled = false;
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            flg = false;
            audioSource = this.GetComponent<AudioSource>();

        }
        SF_Base_pos = transform.position;
        OldPos = SF_Base_pos;

    }

    // Update is called once per frame
    void Update()
    {
        rot += 2f * Time.deltaTime;
        if (rot <= (3f * Mathf.PI) / 4)
        {
            transform.position = new Vector3(SF_Base_pos.x + Mathf.Cos(rot + (3 * Mathf.PI) / 2f) * 3f,
                                             SF_Base_pos.y + Mathf.Sin(2f * (rot + (3 * Mathf.PI) / 2f)) * 1.5f,
                                             SF_Base_pos.z + Mathf.Cos(rot + (3 * Mathf.PI) + Mathf.PI) * 0.5f);
        }
        else
        {
            SF_Base_pos.z += 0.05f;
            Vector3 moveVec = SF_Base_pos - transform.position;
            moveVec.Normalize();
            transform.position += moveVec * 7f * Time.deltaTime;
            float rate = transform.localScale.y * 4f * Time.deltaTime;

            transform.localScale -= new Vector3(rate, rate, rate);
            MainCamera.transform.position += new Vector3(0f, 0f, 4f) * Time.deltaTime;

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }


        Vector3 Dir = transform.position - OldPos;

        if (Dir == Vector3.zero)
            return;

        var targetRot = Quaternion.LookRotation(Dir, Vector3.up);

        // Œ»Ý‚ÌŒü‚«‚Æis•ûŒü‚Æ‚ÌŠp“x·‚ðŒvŽZ
        var diffAngle = Vector3.Angle(transform.forward, Dir);
        // Œ»ÝƒtƒŒ[ƒ€‚Å‰ñ“]‚·‚éŠp“x‚ÌŒvŽZ
        var rotAngle = Mathf.SmoothDampAngle(
            0,
            diffAngle,
            ref _currentAngularVelocity,
            _smoothTime,
            _maxAngularSpeed
        );
        // Œ»ÝƒtƒŒ[ƒ€‚É‚¨‚¯‚é‰ñ“]‚ðŒvŽZ
        var nextRot = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotAngle
        );

        // ƒIƒuƒWƒFƒNƒg‚Ì‰ñ“]‚É”½‰f
        transform.rotation = nextRot;

        OldPos = transform.position;
    }
}
