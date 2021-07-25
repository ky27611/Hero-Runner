using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum MoveType
    {
        Idle,
        Horizontal,
        Vertical,
        Depth,
    }

    public EnemySetting Setting;
    private Dictionary<MoveType, EnemyMove> m_MoveMap = new Dictionary<MoveType, EnemyMove>();

    private EnemyMove m_CurrentMove;

    public float EnemyHP;
    public float EnemyAtk;

    private GameObject Score;
    private GameObject Gamedirector;

    public AudioSource myAudio;
    public AudioClip DefeatSE;

    // Start is called before the first frame update
    void Start()
    {
        m_MoveMap.Add(MoveType.Idle, new EnemyMove(Setting, GetComponent<Transform>()));
        m_MoveMap.Add(MoveType.Horizontal, new HorizontalMove(Setting, GetComponent<Transform>()));
        m_MoveMap.Add(MoveType.Vertical, new VerticalMove(Setting, GetComponent<Transform>()));
        m_MoveMap.Add(MoveType.Depth, new DepthMove(Setting, GetComponent<Transform>()));

        //this.myAudio = GetComponent<AudioSource>();

        switch (Setting.Type)
        {
            case EnemySetting.EnemyType.Slime:
                ChangeMove(MoveType.Horizontal);
                break;
            case EnemySetting.EnemyType.Slime2:
                ChangeMove(MoveType.Depth);
                break;

            case EnemySetting.EnemyType.Turtle:
                ChangeMove(MoveType.Idle);
                break;

            case EnemySetting.EnemyType.Turtle2:
                ChangeMove(MoveType.Horizontal);
                break;

            case EnemySetting.EnemyType.Purple:
                ChangeMove(MoveType.Idle);
                break;

            case EnemySetting.EnemyType.Purple2:
                ChangeMove(MoveType.Depth);
                break;
        }

        this.EnemyHP = Setting.Hp;
        this.EnemyAtk = Setting.Atk;
        
        this.Score = GameObject.Find("ScoreDirector");
        this.Gamedirector = GameObject.Find("GameDirector");

        //Setting.myTransform = this.GetComponent<Transform>();

        /*
        if (Setting.Type == Slime)
        {
            ChangeMove(MoveType.Horizontal);
        }
        else if (Setting.Type == Turtle)
        {
            ChangeMove(MoveType.Depth);
        }
        else if (Setting.Type == Purple)
        {
            ChangeMove(MoveType.Verticle);
        }
        */

        //m_CurrentMove = m_MoveMap[MoveType.Horizontal];
        //m_CurrentMove = m_MoveMap[MoveType.Vertical];
        //m_CurrentMove = m_MoveMap[MoveType.Depth];
        //m_CurrentMove = m_MoveMap[MoveType.Idle];
        //m_CurrentMove.Initialize();
    }

    private void ChangeMove(MoveType move)
    {
        m_CurrentMove?.Release();
        m_CurrentMove = m_MoveMap[move];
        m_CurrentMove.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentMove.OnUpdate();

        if(this.EnemyHP <= 0)
        {
            this.Score.GetComponent<ScoreController>().DefeatEnemy();
            //this.myAudio.PlayOneShot(DefeatSE);
            AudioSource.PlayClipAtPoint(DefeatSE, transform.position);
            Destroy(this.gameObject);
        }

        if (this.Gamedirector.GetComponent<GameDirector>().index == GameDirector.Index.PlayerSelect)
        {
            Destroy(this.gameObject);
        }

    }
    
}
