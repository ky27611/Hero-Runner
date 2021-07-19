using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum StateType
    {
        Idle = 0,
        Jump,
        Sliding,
        Attack,
        Damage,
        Death,
        BossBattle,
    }

    public PlayerSettingData Setting;
    public InitialStatus initialStatus;
    public PlayerSettingComponent Compo;

    private Dictionary<StateType, PlayerState> m_StateMap = new Dictionary<StateType, PlayerState>();

    private PlayerState m_CurrentState;

    private EnemySetting e_Setting;

    private GameObject gamedirector;
    public GameObject ShockWavePrefab;

    //Score
    private GameObject score;
    //Bossオブジェクト
    private GameObject BossSlime;

    public float PlayerHP;

    public AudioClip RunningSE;
    public AudioClip SlidingSE;
    public AudioClip AttackSE;
    public AudioClip PowerUpSE;
    public AudioClip PowerUpMAXSE;
    public AudioClip PowerDownSE;
    public AudioClip DamageSE;

    public AudioClip Null;

    public bool isRunning;
    //public bool isGround;
    //public bool isSESwitch;

    // Start is called before the first frame update
    void Start()
    {
        Setting = new PlayerSettingData(initialStatus);
        Compo = new PlayerSettingComponent();

        m_StateMap.Add(StateType.Idle, new PlayerState(Setting, Compo));
        m_StateMap.Add(StateType.Jump, new PlayerStateJump(Setting, Compo));
        m_StateMap.Add(StateType.Sliding, new PlayerStateSliding(Setting, Compo));
        m_StateMap.Add(StateType.Attack, new PlayerStateAttack(Setting, Compo));
        m_StateMap.Add(StateType.Damage, new PlayerStateDamage(Setting, Compo));
        m_StateMap.Add(StateType.Death, new PlayerStateDeath(Setting, Compo));
        m_StateMap.Add(StateType.BossBattle, new PlayerStateBossBattle(Setting, Compo));

        m_CurrentState = m_StateMap[StateType.Idle];

        //アニメータコンポーネントを取得
        Compo.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        //Setting.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得
        Compo.myRigidbody = GetComponent<Rigidbody>();

        //CapsuleColiderコンポーネントを取得
        Compo.myCollider = GetComponent<CapsuleCollider>();

        //BoxColiderコンポーネントを取得
        //Compo.atkCollider = GetComponent<BoxCollider>();

        Compo.myAudio = GetComponent<AudioSource>();

        this.gamedirector = GameObject.Find("GameDirector");

        //Score
        this.score = GameObject.Find("ScoreDirector");

        //Boss
        this.BossSlime = GameObject.Find("BossSlime");

        this.PlayerHP = Setting.PlayerHP;

        this.isRunning = false;
        //this.isGround = false;
        //this.isSESwitch = true;

        Compo.myAudio.clip = RunningSE;
    }

    public void ChangeState(StateType state)
    {
        m_CurrentState.Release();
        m_CurrentState = m_StateMap[state];
        m_CurrentState.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        this.PlayerHP = Setting.PlayerHP;

        if (this.gameObject.transform.position.x > 6)
        {
            this.gameObject.transform.position = new Vector3(6, 0.1f, this.transform.position.z);
        }
        else if (this.gameObject.transform.position.x < -6)
        {
            this.gameObject.transform.position = new Vector3(-6, 0.1f, this.transform.position.z);
        }

        if (this.isRunning == true)
        {
            Compo.myAnimator.SetFloat("Speed", 1);

            
            if (Setting.isGround == true && Setting.isSliding == false)
            {
                Compo.myAudio.clip = RunningSE;
            }
            else
            {
                Compo.myAudio.clip = Null;
            }

            if (Compo.myAudio.isPlaying == false)
            {
                Compo.myAudio.Play();
            }

            if (this.gamedirector.GetComponent<GameDirector>().index == GameDirector.Index.NormalMode || this.gamedirector.GetComponent<GameDirector>().index == GameDirector.Index.BossMode)
            {
                //横方向の入力による速度
                //float inputVelocityY = 0;

                //プレイヤーを矢印キーまたはボタンに応じて左右に移動させる
                if (Input.GetKeyDown(KeyCode.LeftArrow) && -Setting.movableRange < this.transform.position.x && Setting.movableX == true)
                {
                    Setting.movableX = false;
                    Setting.nowpositionX -= Setting.setpositionX;
                    Setting.inputVelocityX = -Setting.velocityX;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && this.transform.position.x < Setting.movableRange && Setting.movableX == true)
                {
                    Setting.movableX = false;
                    Setting.nowpositionX += Setting.setpositionX;
                    Setting.inputVelocityX = Setting.velocityX;
                }

                //横方向移動したら停止する（3か所）
                if (Setting.inputVelocityX < 0)
                {
                    if (this.transform.position.x <= Setting.nowpositionX)
                    {
                        Setting.inputVelocityX = 0;
                        this.transform.position = new Vector3(Setting.nowpositionX, this.transform.position.y, this.transform.position.z);
                        Setting.movableX = true;
                    }
                }
                else
                {
                    if (this.transform.position.x >= Setting.nowpositionX)
                    {
                        Setting.inputVelocityX = 0;
                        this.transform.position = new Vector3(Setting.nowpositionX, this.transform.position.y, this.transform.position.z);
                        Setting.movableX = true;
                    }
                }

                m_CurrentState.OnUpdate();

                //ジャンプ
                if (Input.GetKeyDown(KeyCode.UpArrow) &&
                    (Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running") || Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sliding")))
                {
                    ChangeState(StateType.Jump);
                }

                //スライディング
                if (Input.GetKeyDown(KeyCode.DownArrow) &&
                    (Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running") || Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
                {
                    ChangeState(StateType.Sliding);
                    Compo.myAudio.PlayOneShot(SlidingSE);

                }

                //攻撃
                if (Input.GetKeyDown(KeyCode.Space) &&
                    (Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running") || Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
                {
                    ChangeState(StateType.Attack);
                    Compo.myAudio.PlayOneShot(AttackSE);

                    if (this.gamedirector.GetComponent<GameDirector>().index == GameDirector.Index.BossMode)
                    {
                        GameObject ShockWave = Instantiate(ShockWavePrefab);
                        ShockWave.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z + 2);
                    }
                    
                }

                //力尽きた
                if (Setting.PlayerHP <= 0)
                {
                    ChangeState(StateType.Death);
                    //this.gamedirector.GetComponent<GameDirector>().index = GameDirector.Index.GameOver;
                    this.gamedirector.GetComponent<GameDirector>().isGameOver = true;
                    Compo.myAudio.Stop();
                }


                /*
                //ボスバトル
                if (this.gamedirector.GetComponent<GameDirector>().index == GameDirector.Index.BossMode)
                {
                    this.score = GameObject.Find("ScoreDirector");
                    this.score.GetComponent<ScoreController>().isTimeScore = false;
                    Setting.velocityZ = 0;
                    //Setting.myAnimator.SetFloat("Speed", 0);
                }
                */


                //Jumpステートの場合はJumpにfalseをセットする
                if (Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    ChangeState(StateType.Idle);
                }
                //Slideステートの場合はSlideにfalseをセットする
                /*if (Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
                {
                    ChangeState(StateType.Idle);
                }
                */

                if (Setting.slidedelta > Setting.slidespan)
                {
                    ChangeState(StateType.Idle);
                }



                if (Setting.atkdelta >= Setting.atkspan)
                {
                    ChangeState(StateType.Idle);
                }
            }
            
            //プレイヤーに速度を与える
            Compo.myRigidbody.velocity = new Vector3(Setting.inputVelocityX, Compo.myRigidbody.velocity.y, Setting.velocityZ);
        }
        else
        {
            Compo.myAnimator.SetFloat("Speed", 0);
            Compo.myAudio.Stop();
            //Compo.myAudio.clip = RunningSE;
            //ChangeState(StateType.Idle);

        }
        
    }
    
    //攻撃当たった時
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Setting.PlayerHP -= 1;
            Compo.myAudio.PlayOneShot(DamageSE);
            //Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "enemy1")
        {
            Setting.PlayerHP -= other.gameObject.GetComponent<Enemy>().EnemyAtk;
            Compo.myAudio.PlayOneShot(DamageSE);
            //Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Boss")
        {
            Setting.PlayerHP -= other.gameObject.GetComponent<Boss>().BossAtk;
            Compo.myAudio.PlayOneShot(DamageSE);
        }
        else if (other.gameObject.tag == "Crystal")
        {
            gamedirector.GetComponent<GameDirector>().HeroPoint += 1;

            if (this.gamedirector.GetComponent<GameDirector>().HeroPointMAX == this.gamedirector.GetComponent<GameDirector>().HeroPoint)//this.gamedirector.GetComponent<GameDirector>().HeroPointRatio >= 1)
            {
                Compo.myAudio.PlayOneShot(PowerUpMAXSE);
            }
            else
            {
                Compo.myAudio.PlayOneShot(PowerUpSE);
            }


            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Flower")
        {
            Compo.myAudio.PlayOneShot(PowerDownSE);
            gamedirector.GetComponent<GameDirector>().HeroPoint -= 1;
            if (gamedirector.GetComponent<GameDirector>().HeroPoint < 0)
            {
                gamedirector.GetComponent<GameDirector>().HeroPoint = 0;
                
            }

            Destroy(other.gameObject);
        }
        /*
        if(other.gameObject.tag == "enemy1")
        {
            other.gameObject.GetComponent<Enemy>().EnemyHP -= Setting.PlayerAtk;
        }
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().BossHP -= Setting.PlayerAtk;
        }
        */
    }

    /*
    //攻撃あたったとき
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Setting.PlayerHP -= 1;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "enemy1")
        {
            Setting.PlayerHP -= other.gameObject.GetComponent<Enemy>().EnemyAtk;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Boss")
        {
            Setting.PlayerHP -= other.gameObject.GetComponent<Boss>().BossAtk;
        }
    }
    */
}