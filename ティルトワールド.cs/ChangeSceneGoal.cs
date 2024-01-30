using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneGoal : MonoBehaviour
{
    
    public string NextScene;
    private GameObject Player;
    private GameObject UI;
    private GameObject SFairy;
    private GameObject Camera;
    private bool GoalFlag;
    private Vector3 StartFailyPos;
    private int XCount;
    private float OldInput;
    private CanvasGroup canvasGroup;
    private bool OnStage;
    protected AudioSource source;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    private void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        Player = GameObject.FindGameObjectWithTag("Player");
        SFairy = GameObject.FindGameObjectWithTag("SFairy");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        GoalFlag = false;
        XCount = 0;
        OldInput = 0f;
        OnStage = false;
        source = gameObject.GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Player.GetComponent<fairy>().AnimEndFlag == true)
        {
            if (GoalFlag == false)
            {
                UI.SetActive(true);
                SFairy.SetActive(true);
                SFairy.transform.position = Camera.transform.position;
                SFairy.transform.position = new Vector3(SFairy.transform.position.x - 220, SFairy.transform.position.y - 50, SFairy.transform.position.z + 500);
                StartFailyPos = SFairy.transform.position;
                GoalFlag = true;
            }
            float y = Input.GetAxis("Vertical");
            if (y * y < 0.2f * 0.2f)
            {
                y = 0f;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || y > 0f && OldInput <= 0f)
            {
                if (XCount > 0)
                {
                    SFairy.transform.position += new Vector3(0.0f, 50.0f, 0.0f);
                    XCount--;
                    source.PlayOneShot(clip1);
                }
;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || y < 0f && OldInput >= 0f)
            {
                if (XCount < 2)
                {
                    SFairy.transform.position -= new Vector3(0.0f, 50.0f, 0.0f);
                    XCount++;
                    source.PlayOneShot(clip1);

                }
            }

            OldInput = y;
            
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                source.PlayOneShot(clip2);


                if (XCount == 0)
                {
                    if(SceneManager.GetActiveScene().name == "Tutorial" || SceneManager.GetActiveScene().name == "S-EX")
                        operation_Fairy.nowStage = 0;
                    else
                        ++operation_Fairy.nowStage;

                    Initiate.Fade(NextScene, Color.white, 1.0f);
                }
                if (XCount == 1)
                {
                    Initiate.Fade("StageSelect", Color.white, 1.0f);
                }
                if (XCount == 2)
                {
                    Initiate.Fade("Title", Color.white, 2.0f);
                }
                OnStage = true;
            }
            if (OnStage)
            canvasGroup.alpha -= 2f * Time.deltaTime;
        }
        else
        {
            UI.SetActive(false);
            SFairy.SetActive(false);
        }
    }

}
