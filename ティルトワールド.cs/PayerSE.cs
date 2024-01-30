using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerSE : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] float pitchRange = 0.1f;
    protected AudioSource source;
    private Vector3 oldPosition;
    float oldvec;
    private void Awake()
    {
        source = this.GetComponent<AudioSource>();
        oldPosition = transform.position;
        source.clip = clip;
        oldvec = 0;

    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 moveVec = transform.position - oldPosition;
       
        oldPosition = transform.position;
        if (oldvec != moveVec.sqrMagnitude)
        {
            if (moveVec.sqrMagnitude > 0)
            {
                source.pitch = 0.6f;
                source.volume = 0.0f;
                source.Play();
            }
            else
            {
                source.Stop();
            }
        }

        float vol = moveVec.sqrMagnitude / 10.0f;
        if (vol > 1)
            vol = 1f;
        

        source.volume = vol;

        oldvec = moveVec.sqrMagnitude;

        //É|Å[ÉYíÜÇÕâπÇ™Ç»ÇÁÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        if (Time.timeScale == 0f)
            source.Stop();
    }

}
