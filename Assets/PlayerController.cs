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
    }

    private PlayerSetting Setting;

    private Dictionary<StateType, PlayerState> m_StateMap = new Dictionary<StateType, PlayerState>();

    private PlayerState m_CurrentState;

    private GameObject gamedirector;

    //Score
    private GameObject score;
    //Bossオブジェクト
    private GameObject BossCube;
    //BossのHP（仮）
    private int BossHP = 3;
    //playerの攻撃力（仮）
    private int playeratk = 1;
    //ゴール位置
    private float goalpos;
    //boss戦闘中状態
    public bool bossbattlestate = false;


    // Start is called before the first frame update
    void Start()
    {
        Setting = new PlayerSetting();

        m_StateMap.Add(StateType.Idle, new PlayerState(Setting));
        m_StateMap.Add(StateType.Jump, new PlayerStateJump(Setting));
        m_StateMap.Add(StateType.Sliding, new PlayerStateSliding(Setting));
        m_StateMap.Add(StateType.Attack, new PlayerStateAttack(Setting));
        m_StateMap.Add(StateType.Damage, new PlayerStateDamage(Setting));
        m_StateMap.Add(StateType.Death, new PlayerStateDeath(Setting));

        m_CurrentState = m_StateMap[StateType.Idle];

        //アニメータコンポーネントを取得
        Setting.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        Setting.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得
        Setting.myRigidbody = GetComponent<Rigidbody>();

        //CapsuleColiderコンポーネントを取得
        Setting.myCollider = GetComponent<CapsuleCollider>();

        //BoxColiderコンポーネントを取得
        Setting.atkCollider = GetComponent<BoxCollider>();

        this.gamedirector = GameObject.Find("GameDirector");

        //Score
        this.score = GameObject.Find("ScoreDirector");

        //Boss
        this.BossCube = GameObject.Find("BossCube");

        //ゴール位置
        this.goalpos = BossCube.transform.position.z;

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
        if(Setting.inputVelocityX < 0)
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
            (Setting.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion") || Setting.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            ChangeState(StateType.Jump);
        }

        //スライディング
        if (Input.GetKeyDown(KeyCode.DownArrow) &&
            (Setting.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion") || Setting.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            ChangeState(StateType.Sliding);
        }

        //攻撃
        if (Input.GetKeyDown(KeyCode.Space) && 
            (Setting.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion") || Setting.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            ChangeState(StateType.Attack);
        }

        //ゲームオーバー
        if (Setting.playerHP <= 0)
        {
            ChangeState(StateType.Death);
        }

        //ボス前は停止
        if (this.transform.position.z - goalpos >= -5)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, goalpos - 5);
            Setting.velocityZ = 0;
            Setting.myAnimator.SetFloat("Speed", 0);
            this.score.GetComponent<ScoreController>().isTimeScore = false;
        }
        

        //Jumpステートの場合はJumpにfalseをセットする
        if (Setting.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            ChangeState(StateType.Idle);
        }
        //Slideステートの場合はSlideにfalseをセットする
        if (Setting.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            ChangeState(StateType.Idle);
        }

        //プレイヤーに速度を与える
        Setting.myRigidbody.velocity = new Vector3(Setting.inputVelocityX, Setting.myRigidbody.velocity.y, Setting.velocityZ);
    }

    //攻撃当てたとき
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy1")
        {
            this.score.GetComponent<ScoreController>().DefeatEnemy();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Boss")
        {
            BossHP -= playeratk;
            Debug.Log(BossHP);

            if (BossHP <= 0)
            {
                this.score.GetComponent<ScoreController>().DefeatBoss();
                Destroy(other.gameObject);
                this.gamedirector.GetComponent<GameDirector>().isClear = true;

            }
        }
    }

    //攻撃あたったとき
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cube" || other.gameObject.tag == "enemy1" || other.gameObject.tag == "Boss")
        {
            Setting.playerHP -= 1;
        }

    }

}