using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TextManager : MonoBehaviour
{

    public GameObject TextObj;
    public GameObject Gakubuti;
    public GameObject Light;
    public GameObject StageCore;

    private GameObject GameObjectRot;

    private TextMeshProUGUI Text;
    private Playersize GameoveredSize;

    public int FontSize;
    private int TutorialFlag;
    private float Count;
    public float DelayDuration = 0.1f;
    private bool IsRunning;
    private float RemainTime;
    private int CurrentMaxVisibleCharacters;
    private bool Delay;
    [SerializeField] AudioSource Source;
    [SerializeField] AudioClip Clip1;
    void Start()
    {
        Text = TextObj.GetComponent<TextMeshProUGUI>();
        GameoveredSize = GetComponent<Playersize>();
        GameObjectRot = GameObject.FindGameObjectWithTag("Rot");

        TutorialFlag = 0;
        Time.timeScale = 1f;
        Count = 0;
        FontSize = 80;
        Text.maxVisibleCharacters = 0;
        IsRunning = true;
        RemainTime = DelayDuration;
        CurrentMaxVisibleCharacters = 0;
        Delay = false;
        Gakubuti.SetActive(false);
        Source = gameObject.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Count);
        if (!Delay)
            Count += Time.deltaTime;
        if (Count > 2 && !Delay)
        {
            Delay = true;
            Time.timeScale = 0f;
            Count = 0f;

            if (!Source.isPlaying || Source.volume != 1.0f)
            {
                Source.Stop();
                Source.pitch = 1.0f;
                Source.volume = 1.0f;
                Source.PlayOneShot(Clip1);
            }

        }
        if (Delay)
        {
            Text.fontSize = FontSize;

            if (Mathf.Approximately(Time.timeScale, 1f))
            {
                Gakubuti.SetActive(false);
                Light.GetComponent<Light>().intensity = 1f;
            }
            else
            {
                Gakubuti.SetActive(true);
                Light.GetComponent<Light>().intensity = 0f;
                if (!Source.isPlaying || Source.volume != 1.0f)
                {
                    Source.Stop();
                    Source.pitch = 1.0f;
                    Source.volume = 1.0f;
                    Source.PlayOneShot(Clip1);
                }
            }

            switch (TutorialFlag)
            {
                case 0:
                    Text.text = "���X�e�B�b�N�ŃX�e�[�W���X���]����܂�";
                    break;
                case 1:
                    Text.text = "�E�X�e�B�b�N�ŃX�e�[�W������]�o���܂�";
                    break;
                case 2:
                    Text.text = "��ɓ�����Ƒ傫���Ȃ�܂��Ⴊ�Ȃ��Ƃ���œ]�����ď������Ȃ肷�����GAMEOVER";
                    break;
                case 3:
                    Text.text = "�X�e�[�W���X����ƕǂɓo��܂�";
                    break;
                case 4:
                    Text.text = "�X�e�[�W���X�����������g���ăW�����v�o���܂�";
                    break;
                case 5:
                    Text.text = "�X�e�[�W�𔽓]����Ɨ����ɂ��\��t���܂�";
                    break;
                case 6:
                    Text.text = "�S�[���͐�ʂł�������ƃX�e�[�W�N���A!!";
                    break;
                case 7:
                    Text.text = "";
                    break;
            }


            if (GameoveredSize.dedflg)
            {
                switch (TutorialFlag)
                {
                    case 0:
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        break;
                    case 1:
                        GameOver();
                        transform.position = new Vector3(GameObjectRot.transform.position.x - 400, GameObjectRot.transform.position.y, GameObjectRot.transform.position.z);
                        break;
                    case 2:
                        GameOver();
                        GameObject gameObjectSnow = GameObject.FindGameObjectWithTag("TSnow");
                        transform.position = gameObjectSnow.transform.position;
                        break;
                    case 3:
                        GameOver();
                        GameObject gameObjectWall = GameObject.FindGameObjectWithTag("Wall");
                        transform.position = gameObjectWall.transform.position;
                        break;
                    case 4:
                        GameOver();
                        GameObject gameObjectJump = GameObject.FindGameObjectWithTag("Jump");
                        transform.position = gameObjectJump.transform.position;
                        break;
                    case 5:
                        GameOver();
                        GameObject gameObjectRear = GameObject.FindGameObjectWithTag("Rear");
                        transform.position = gameObjectRear.transform.position;
                        break;
                    case 6:
                        GameOver();
                        GameObject gameObjectGoal = GameObject.FindGameObjectWithTag("TGoal");
                        transform.position = gameObjectGoal.transform.position;
                        break;
                    case 7:
                        Text.text = "";
                        break;
                }
            }


            // ���o���s���łȂ���Ή������Ȃ�
            if (!IsRunning) return;

            // ���̕����\���܂ł̎c�莞�ԍX�V
            RemainTime -= Time.unscaledDeltaTime;
            if (RemainTime > 0) return;

            // �\�����镶����������₷
            Text.maxVisibleCharacters = ++CurrentMaxVisibleCharacters;
            RemainTime = DelayDuration;

            // ������S�ĕ\��������ҋ@��ԂɈڍs
            if (CurrentMaxVisibleCharacters >= Text.text.Length)
            {
                if (Count >= 10)
                {
                    Time.timeScale = 1f;
                    IsRunning = false;
                    Count = 0;
                }
                Count++;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Rot"))
        {
            if (TutorialFlag == 0)
            {
                TutorialFlag = 1;
                InitAnim();
            }
        }

        if (other.gameObject.CompareTag("TSnow"))
        {
            if (TutorialFlag == 1)
            {
                TutorialFlag = 2;
                InitAnim();
            }
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            if (TutorialFlag == 2)
            {
                TutorialFlag = 3;
                InitAnim();
            }
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            if (TutorialFlag == 3)
            {
                TutorialFlag = 4;
                InitAnim();
            }
        }

        if (other.gameObject.CompareTag("Rear"))
        {
            if (TutorialFlag == 4)
            {
                TutorialFlag = 5;
                InitAnim();
            }
        }

        if (other.gameObject.CompareTag("TGoal"))
        {
            if (TutorialFlag == 5)
            {
                TutorialFlag = 6;
                InitAnim();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Goal"))
        {
            if (TutorialFlag == 6)
            {
                TutorialFlag = 7;
                Count = 10;
                InitAnim();
            }
        }
    }

    void InitAnim()
    {
        Time.timeScale = 0f;
        Text.maxVisibleCharacters = 0;
        CurrentMaxVisibleCharacters = 0;
        IsRunning = true;
    }
    void GameOver()
    {
        StageCore.transform.up = Vector3.up;
        transform.localScale = new Vector3(200, 200, 200);
        GameoveredSize.dedflg = false;
    }

}
