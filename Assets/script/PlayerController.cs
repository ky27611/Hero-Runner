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

    //Score
    private GameObject score;
    //Bossオブジェクト
    private GameObject BossSlime;

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
        Compo.atkCollider = GetComponent<BoxCollider>();

        this.gamedirector = GameObject.Find("GameDirector");

        //Score
        this.score = GameObject.Find("ScoreDirector");

        //Boss
        this.BossSlime = GameObject.Find("BossSlime");
    }

    private void ChangeState(StateType state)
    {
        m_CurrentState.Release();
        m_CurrentState = m_StateMap[state];
        m_CurrentState.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gamedirector.GetComponent<GameDirector>().isGameStart == false)
        {
            Compo.myAnimator.SetFloat("Speed", 1);

            //横方向の入力による速度
            float inputVelocityY = 0;

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
            }

            //攻撃
            if (Input.GetKeyDown(KeyCode.Space) &&
                (Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running") || Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                ChangeState(StateType.Attack);
            }

            //力尽きた
            if (Setting.PlayerHP <= 0)
            {
                ChangeState(StateType.Death);
            }

            //ボスバトル
            if(gamedirector.GetComponent<GameDirector>().isBossBattle == true)
            {
                this.score = GameObject.Find("ScoreDirector");
                this.score.GetComponent<ScoreController>().isTimeScore = false;
                Setting.velocityZ = 0;
                //Setting.myAnimator.SetFloat("Speed", 0);
            }

            
            //Jumpステートの場合はJumpにfalseをセットする
            if (Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                ChangeState(StateType.Idle);
            }
            //Slideステートの場合はSlideにfalseをセットする
            if (Compo.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
            {
                ChangeState(StateType.Idle);
            }
            
            
            if(Setting.atkdelta >= Setting.atkspan)
            {
                ChangeState(StateType.Idle);
            }

            //プレイヤーに速度を与える
            Compo.myRigidbody.velocity = new Vector3(Setting.inputVelocityX, Compo.myRigidbody.velocity.y, Setting.velocityZ);
        }
        else
        {
            ChangeState(StateType.Idle);
        }
    }
    
    //攻撃当てたとき
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy1")
        {
            other.gameObject.GetComponent<Enemy>().EnemyHP -= Setting.PlayerAtk;
        }
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().BossHP -= Setting.PlayerAtk;
        }
    }

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
}