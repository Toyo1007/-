using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{
    private GameObject pauseUI;

    // 点滅させる対象
    private Behaviour[] Target = new Behaviour[3];

    private int Count;
    // 点滅周期[s]
    [SerializeField] private float Cycle = 1;

    private double Time;

    private float OldStickVec;

    private CanvasGroup CanvasGroup;

    private AudioSource source;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI = GameObject.FindGameObjectWithTag("Pause");

        for (int i = 0; i < 3; i++)
        {
            Target[i] = pauseUI.transform.GetChild(i).GetComponent<Behaviour>();
        }

        //　スタート時にポーズUIを非表示にする
        pauseUI.SetActive(false);

        Count = 0;
        OldStickVec = 0f;
        CanvasGroup = this.GetComponent<CanvasGroup>();
        source = pauseUI.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        //　ポーズボタンを押した時
        if (Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown("joystick button 7"))
        {
            if (Mathf.Approximately(Time.timeScale, 1f))
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
                source.PlayOneShot(clip1);

            }
            else
            {
                Time.timeScale = 1f;
                source.PlayOneShot(clip1);
                pauseUI.SetActive(false);

            }
        }
        if (pauseUI.activeSelf)
        {
            float y = Input.GetAxis("Vertical");
            if (y * y < 0.2f * 0.2f)
            {
                y = 0f;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || y < 0 && OldStickVec >= 0)
            {
                if (Count < Target.Length - 1)
                {
                    Count++;
                    source.PlayOneShot(clip2);

                }
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || y > 0 && OldStickVec <= 0)
            {
                if (Count > 0)
                {
                    Count--;
                    source.PlayOneShot(clip2);

                }
            }
            OldStickVec = y;
            // 内部時刻を経過させる
            Time += Time.unscaledDeltaTime;

            // 周期cycleで繰り返す値の取得
            // 0～cycleの範囲の値が得られる
            var repeatValue = Mathf.Repeat((float)Time, Cycle);

            // 点滅させるUIの設定
            for (int i = 0; i < Target.Length; i++)
            {
                if (Target[i] != null)
                {
                    if (i == Count)
                    {
                        Target[i].enabled = repeatValue >= Cycle * 0.5f;
                    }
                    else
                    {
                        Target[i].enabled = true;
                    }
                }
            }
        }

        //ボタン選択されたとき
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            source.PlayOneShot(clip1);

            switch (Count)
            {
                case 0:
                    Time.timeScale = 1f;
                    pauseUI.SetActive(false);
                    break;
                case 1:
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case 2:
                    Time.timeScale = 1f;
                    SceneManager.LoadScene("StageSelect");
                    break;

            }


        }
    }
}