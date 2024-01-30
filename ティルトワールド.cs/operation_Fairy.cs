using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class operation_Fairy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 0f;
    static public bool[] StageClear = new bool[31];
    public Vector3 MovePos_Fairy;
    public Vector3 CameraPos;
    public int Xcount = 0;//左からの移動距離
    public int Ycount = 0;//上からの移動距離
    public int Ncount = 0;//現在の棚の位置
    private Vector3 BasePos_Fairy;
    private float rot;//妖精上下移動用の回転変数
    private bool Xhorflg = false;
    private bool Yhorflg = false;
    private Vector3 OldPos;
    private float _smoothTime = 0.1f;
    private float _currentAngularVelocity;
    private float _maxAngularSpeed = Mathf.Infinity;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    static public int nowStage = 0;

    void Start()
    {
        BasePos_Fairy = new Vector3(-6.55f,21.42f, -2.48f);
        MovePos_Fairy = BasePos_Fairy;
        OldPos = BasePos_Fairy;
        audioSource = this.GetComponent<AudioSource>();
        CameraPos = BasePos_Fairy;

        for (int i = 0; i < nowStage; i++)
        {
            Xcount++;
            if(Xcount > 4)
            {
                Xcount = 0;
                Ycount++;
                if(Ycount > 3)
                {
                    Ncount++;
                    Xcount = 0;
                    Ycount = 0;
                }
            }
        }

        MovePos_Fairy = new Vector3(BasePos_Fairy.x + (float)Xcount * 3.275f + Ncount * 20f,
                                         BasePos_Fairy.y - (float)Ycount * 3.83f,
                                         BasePos_Fairy.z);
    }

    // Update is called once per frame
    void Update()

    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            x = -1f;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            x = 1f;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            y = 1f;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            y = -1f;
        if (Mathf.Sqrt(x * x + y * y) < 0.5f)
        {
            x = 0f;
            y = 0f;
        }
        if (x == 0 && y == 0)
        {
            Xhorflg = false;
            x = 0f;
            Yhorflg = false;
            y = 0f;
        }

        if (x*x > y*y)
        {
            Yhorflg = false;
            y = 0f;
            if (x != 0)
            {
                float sign = Mathf.Abs(x) / x;
                x = sign;
                if (!Xhorflg)
                {

                    Xhorflg = true;
                    Xcount += (int)sign;
                    bool OnSEflg =true;
                    if (Xcount > 4)
                    {
                        if (Ncount == 0)
                        {
                            Ncount++;
                            Xcount = 0;
                            Ycount = 0;
                        }
                        else
                        {
                            Xcount = 4;
                            OnSEflg = false;
                        }
                    }
                    else if (Xcount < 0)
                    {
                        if (Ncount == 1)
                        {
                            Ncount--;
                            Xcount = 4;
                            Ycount = 3;

                        }
                        else
                        {
                            Xcount = 0;
                            OnSEflg = false;
                        }
                    }
                    else if(Ncount == 1)
                    {
                        if(Ycount == 2)
                            if(Xcount == 1)
                            {
                                Xcount = 0;
                            }

                    }
                    if (OnSEflg)
                        audioSource.PlayOneShot(audioClip);
                }
            }

        }
        else
        {
            Xhorflg = false;
            x = 0f;
            if (y != 0)
            {
                float sign = Mathf.Abs(y) / y;
                y = sign;
                if (!Yhorflg)
                {
                    Yhorflg = true;
                    Ycount -= (int)sign;
                    bool onSEflg =false;
                    if (Ycount > 3)
                        Ycount = 3;
                    else if (Ycount < 0)
                        Ycount = 0;
                    else
                        onSEflg = true;

                    if (Ycount >= 2 && Ncount == 1)
                    {
                        if (Xcount != 0)
                        {
                            Ycount = 1;
                            onSEflg = false;
                        }
                        else if(Ycount == 3)
                        {
                            Ycount = 2;
                        }
                    }
                    if(onSEflg)audioSource.PlayOneShot(audioClip);

                }
                
            }


        }
        nowStage = Xcount + Ycount * 5 + Ncount * 20;
        CameraPos = new Vector3(BasePos_Fairy.x + (float)Xcount * 3.275f + Ncount * 20f,
                                         BasePos_Fairy.y - (float)Ycount * 3.83f,
                                         BasePos_Fairy.z);
        MoveFairy(CameraPos, 15);

        transform.position = new Vector3(MovePos_Fairy.x,
                                            MovePos_Fairy.y + (Mathf.Sin(rot / 100f) * 1f ),
                                            MovePos_Fairy.z) ;
        rot += 150f * Time.deltaTime;

        //transform.eulerAngles = new Vector3(y * 300f, x * -300f, 0f);
        Vector3 Dir = MovePos_Fairy - OldPos;

        if (Dir == Vector3.zero)
            return;

        var targetRot = Quaternion.LookRotation(Dir, Vector3.up);

        // 現在の向きと進行方向との角度差を計算
        var diffAngle = Vector3.Angle(transform.forward, Dir);
        // 現在フレームで回転する角度の計算
        var rotAngle = Mathf.SmoothDampAngle(
            0,
            diffAngle,
            ref _currentAngularVelocity,
            _smoothTime,
            _maxAngularSpeed
        );
        // 現在フレームにおける回転を計算
        var nextRot = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotAngle
        );

        // オブジェクトの回転に反映
        transform.rotation = nextRot;

        OldPos = MovePos_Fairy;


    }
    private void MoveFairy(Vector3 movePoint,float speed)
    {
        float Sqrlen = Vector3.SqrMagnitude(movePoint - MovePos_Fairy);
        float r = 0.11f;
        if (Sqrlen < r * r)
        {
            MovePos_Fairy = movePoint;
            OldPos = MovePos_Fairy - Vector3.forward;
        }
        else
        {
            Vector3 MoveVec = movePoint - MovePos_Fairy;
            MoveVec.Normalize();
            MovePos_Fairy += MoveVec * speed * Time.deltaTime;
        }
    }
}


