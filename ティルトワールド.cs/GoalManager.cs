using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    private GameObject StageCore;
    private GameObject Player;
    private GameObject Camera;
    private GameObject Goal;
    private Vector3 SetPlayerPos;//アニメーション終了ポジション
    private bool HitPlayer;
    private Rigidbody RbP;
    private Vector3 PlayerHitSize;//プレイヤーが当たった時の大きさ
    private float UpPower;
    private bool GoalFlag;
    private int Count;
    //private Switch m_switch;
    private GameObject GameSound;
    protected AudioSource source;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;


    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        StageCore = GameObject.Find(" StageCore");
        Player = GameObject.Find("Player");
        Goal = GameObject.FindGameObjectWithTag("Goal");

        RbP = Player.GetComponent<Rigidbody>();
        UpPower = 100000f;
        HitPlayer = false;
        GoalFlag = false;
        GameSound = GameObject.FindGameObjectWithTag("GameSound");
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HitPlayer)
        {
            RbP.velocity = ((SetPlayerPos - Player.transform.position) * 200f * Time.deltaTime + transform.up * UpPower * Time.deltaTime);

            if(UpPower > -10000f)
            UpPower -=  50000f * Time.deltaTime;
            else
            {
                UpPower = 0f;
            }
            //Debug.Log(UpPower);
            Player.transform.localScale = PlayerHitSize;

            if (GoalFlag == false)
            {
                GoalFlag = true;
                Camera.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                Camera.transform.position = new Vector3(transform.position.x, Goal.transform.position.y + 200f, Camera.transform.position.z - 500) ;

            }
            if(!source.isPlaying)
            source.PlayOneShot(clip2);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player && !HitPlayer)
        {
            StageCore.transform.up = Vector3.up;
            StageCore.GetComponent<MoveStageCore>().enabled = false; //プレイヤーの操作停止
            Player.GetComponent<Playersize>().enabled = false;//プレイヤーが小さくなるの防止
            StageCore.GetComponent<YRot>().enabled = false;//プレイヤーが小さくなるの防止
            Player.GetComponent<Gravity>().enabled = false;//重力が働き続けるのを防止
            Player.GetComponent<PauseScript>().enabled = false;//ポーズ画面を開けなくする
            Camera.GetComponent<CameraMove>().enabled = false;//右スティック防止
            SetPlayerPos = transform.position + transform.up * transform.localScale.y / 2;
            HitPlayer = true;
            PlayerHitSize = Player.transform.localScale;
            Player.GetComponent<AudioSource>().enabled = false;
            if (SceneManager.GetActiveScene().name != "Tutorial")
                operation_Fairy.StageClear[operation_Fairy.nowStage] = true;
            //m_switch.GoalPlayer_switch = true;
            if (GameSound)
            {
                GameSound.GetComponent<AudioSource>().enabled = false;
            }
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("Wind");

            foreach (GameObject cube in cubes)
            {
                if(cube)
                cube.GetComponent<AudioSource>().enabled = false;
            }

            source.PlayOneShot(clip1);
        }
    }

}
