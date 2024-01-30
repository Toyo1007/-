using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy_SE : MonoBehaviour
{
    protected AudioSource source;
    [SerializeField] AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying)
        source.PlayOneShot(clip);
    }
}
