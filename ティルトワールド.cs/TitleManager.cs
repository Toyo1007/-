using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    // 点滅させる対象
    private RawImage[] Target = new RawImage[2];

    private int Count = 0;
    [Header("1ループの長さ(秒単位)")]
    [SerializeField]
    [Range(0.1f, 10.0f)]
    float Duration = 1.0f;

    [Header("ループ開始時の色")]
    [SerializeField]
    Color32 StartColor = new Color32(255, 255, 255, 255);

    //ループ終了(折り返し)時の色を0～255までの整数で指定。
    [Header("ループ終了時の色")]
    [SerializeField]
    Color32 EndColor = new Color32(255, 255, 255, 64);


    public CanvasGroup canvasGroup;
    bool oNbutton;
    protected AudioSource source;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    private float oldStickVec;
    private float timer = 0f;

    void OnEnable()
    {
        for (int i = 0; i < 2; i++)
        {
            Target[i] = canvasGroup.transform.GetChild(i).GetComponent<RawImage>();
        }

        Count = 0;
        oNbutton = false;
        source = gameObject.GetComponent<AudioSource>();
        oldStickVec = 0f;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Count != 0 && Count != 1)
        {
            Count = 0;
        }

        float y = Input.GetAxis("Vertical");
        if (y * y < 0.2f * 0.2f)
        {
            y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || y < 0 && oldStickVec >= 0)
        {
            if (Count < Target.Length - 1)
            {
                Count++;
                source.PlayOneShot(clip2);
            }
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || y > 0 && oldStickVec <= 0)
        {
            if (Count > 0)
            {
                Count--;
                source.PlayOneShot(clip2);

            }
        }
        oldStickVec = y;

        for (int i = 0; i < Target.Length; i++)
        {
            if (Target[i] != null)
            {
                if (i == Count)
                {
                    Target[i].color = Color.Lerp(StartColor, EndColor, Mathf.PingPong(Time.time / Duration, 1.0f));
                }
                else
                {
                    Target[i].color = StartColor;
                }
            }
        }

        timer += Time.deltaTime;
        if (timer > 1.0f)
            if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log(Count);
                if (Count == 1)
                    Initiate.Fade("Tutorial", Color.white, 1.0f);
                else
                    Initiate.Fade("StageSelect", Color.white, 1.0f);

                oNbutton = true;
                source.PlayOneShot(clip1);
            }
        if (oNbutton)
            canvasGroup.alpha -= 1.0f * Time.deltaTime;
    }

}