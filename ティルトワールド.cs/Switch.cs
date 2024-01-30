using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool SwitchFlag;
    float RotSpeed;
    protected AudioSource source;
    [SerializeField] AudioClip clip;
    float Timer;
    // Start is called before the first frame update
    void Start()
    {
        RotSpeed = 1000.0f;
        SwitchFlag = false;
        source = gameObject.GetComponent<AudioSource>();
        Timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SwitchFlag == true && Timer < 4f)
        {
            float Rot = RotSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(Rot, Vector3.up);
            Timer += Time.deltaTime;
        }
        if (Timer > 4f)
        {
            source.Stop();
        }
        

        if (!source.isPlaying && SwitchFlag && Timer < 4f)
        {
            source.pitch = 2.5f +Random.Range(-0.3f, 0.3f);
            source.Play();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SwitchFlag = true;
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
            }

        }

    }
}
