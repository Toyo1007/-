using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageSelectManager : MonoBehaviour
{
    public List<string> Stages;
    GameObject Player;
    [SerializeField] Material[] SnowDoom = new Material[30];
    operation_Fairy of;
    SelectAfter_FairyMove SAF;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    // Start is called before the first frame update
    bool isAbottn = false;
    bool isBbottn = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        of = Player.GetComponent<operation_Fairy>();
        SAF = Player.GetComponent<SelectAfter_FairyMove>();
        audioSource = this.GetComponent<AudioSource>();
        isAbottn = false;
        isBbottn = false;
        for (int i = 0; i < 30; i++)
        {
            if (operation_Fairy.StageClear[i])
            {
                SnowDoom[i].color = new Color(1f,1f,1f,0f);
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            if (!isBbottn)
            {
                of.enabled = false;
                SAF.enabled = true;
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(audioClip);
                }
                isAbottn = true;
                Invoke("ChangeScene", 1.2f);
            }
        }
        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isAbottn)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(audioClip);
                }
                isBbottn = true;
                Initiate.Fade("Title", Color.white, 2.0f);
            }
        }

    }

    public void ChangeScene()
    {
        int nowStage = of.Xcount + of.Ycount * 5 + of.Ncount * 20;
        Debug.Log(nowStage);
        Initiate.Fade(Stages[nowStage], Color.white, 1.0f);
    }
}
