using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public enum BossType
    {
        Idle,
        BossSlime,
        BossTurtle,
        BossPurple,
        BossDragon,
    }

    public BossSetting Setting;
    private Dictionary<BossType, BossMove> m_BossMap = new Dictionary<BossType, BossMove>();

    private BossMove m_CurrentMove;

    public float BossHP;
    public float BossLastHP;
    public float BossAtk;

    private GameObject Score;
    private GameObject gameDirector;
    private GameObject Player;
    private Animator myAnimator;
    public bool isAttack;
    public bool isDeath;
    public float waittime;

    public GameObject BossAttack1Prefab;
    public GameObject BossAttack2Prefab;
    public GameObject BossAttack3Prefab;
    public GameObject BossAttack4Prefab;

    public AudioSource myAudio;
    public AudioClip AppearSE;
    public AudioClip DefeatSE;
    public AudioClip DamageSE;


    // Start is called before the first frame update
    void Start()
    {
        m_BossMap.Add(BossType.Idle, new BossMove(Setting));
        m_BossMap.Add(BossType.BossSlime, new BossMove(Setting));
        m_BossMap.Add(BossType.BossTurtle, new BossMove(Setting));
        m_BossMap.Add(BossType.BossPurple, new BossMove(Setting));
        m_BossMap.Add(BossType.BossDragon, new BossMove(Setting));

        switch (Setting.Type)
        {
            case BossSetting.BossType.BossSlime:
                ChangeMove(BossType.BossSlime);
                break;
            case BossSetting.BossType.BossTurtle:
                ChangeMove(BossType.BossTurtle);
                break;
            case BossSetting.BossType.BossPurple:
                ChangeMove(BossType.BossPurple);
                break;
            case BossSetting.BossType.BossDragon:
                ChangeMove(BossType.BossDragon);
                break;
        }

        

        this.isDeath = false;

        this.Score = GameObject.Find("ScoreDirector");
        this.gameDirector = GameObject.Find("GameDirector");
        this.Player = GameObject.Find("Player");
        this.myAudio = GetComponent<AudioSource>();
        this.myAnimator = GetComponent<Animator>();

        this.BossHP = Setting.Hp + (((this.gameDirector.GetComponent<GameDirector>().StageNo - 1) / 10) * 5);
        this.BossLastHP = this.BossHP;
        this.BossAtk = Setting.Atk;

        this.transform.position = new Vector3(0, this.transform.position.y, Player.transform.position.z + 12);
        this.myAudio.PlayOneShot(AppearSE);

    }

    private void ChangeMove(BossType move)
    {
        m_CurrentMove?.Release();
        m_CurrentMove = m_BossMap[move];
        m_CurrentMove.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.BossLastHP > this.BossHP)
        {
            AudioSource.PlayClipAtPoint(DamageSE, transform.position);
        }

        this.BossLastHP = this.BossHP;

        this.transform.position = new Vector3(0, this.transform.position.y, Player.transform.position.z + 12);
        m_CurrentMove.OnUpdate();

        this.waittime += Time.deltaTime;

        if (this.waittime >= Setting.Cooltime )
        {
            isAttack = true;
            this.waittime = 0;
        }
        
        if (isAttack == true)
        {
            Attack();
        }
        
        if (this.BossHP <= 0)
        {
            //this.gameDirector.GetComponent<GameDirector>().isStageClear = true;
            AudioSource.PlayClipAtPoint(DefeatSE, transform.position);
            

            if (isDeath == false)
            {
                this.myAnimator.SetTrigger("DeathTrigger");
                this.gameDirector.GetComponent<GameDirector>().isBossDefeat = true;
            }

            Invoke("Defeat", 2.5f);
        }

        if (this.gameDirector.GetComponent<GameDirector>().index == GameDirector.Index.PlayerSelect)
        {
            Destroy(this.gameObject);
        }
    }

    void Attack()
    {
        int num = Random.Range(1, 101);

        if (num <= Setting.RandomNum)
        {
            switch (Setting.Type)
            {
                case BossSetting.BossType.BossSlime:
                    int num5 = Random.Range(1, 11);
                    if (num5 <= 5)
                    {
                        GameObject BossAttack1 = Instantiate(BossAttack1Prefab);
                        BossAttack1.transform.position = new Vector3(0, this.transform.position.y, Player.transform.position.z + 11);
                    }
                    else
                    {
                        GameObject BossAttack1 = Instantiate(BossAttack1Prefab);
                        BossAttack1.transform.position = new Vector3(0, 1.8f, Player.transform.position.z + 11);
                    }
                    
                    break;
                case BossSetting.BossType.BossTurtle:
                    
                    int num3 = Random.Range(1, 11);
                    GameObject BossAttack2 = Instantiate(BossAttack2Prefab);
                    if (num3 <= 6)
                    {
                        BossAttack2.transform.position = new Vector3(Player.transform.position.x , this.transform.position.y, Player.transform.position.z + 11);
                    }
                    else
                    {
                        int offsetX = Random.Range(-1, 2);
                        BossAttack2.transform.position = new Vector3(2 * offsetX, this.transform.position.y, Player.transform.position.z + 11);
                    }
                    
                    break;
                case BossSetting.BossType.BossPurple:
                    int offsetX2 = Random.Range(-1, 2);
                    GameObject BossAttack3 = Instantiate(BossAttack3Prefab);
                    BossAttack3.transform.position = new Vector3(1.8f * offsetX2, this.transform.position.y, Player.transform.position.z + 11);
                    break;
                case BossSetting.BossType.BossDragon:
                    int num2 = Random.Range(1, 4);
                    switch (num2)
                    {
                        case 1:
                            int num6 = Random.Range(1, 11);
                            if (num6 <= 5)
                            {
                                GameObject BossAttack4 = Instantiate(BossAttack1Prefab);
                                BossAttack4.transform.position = new Vector3(0, this.transform.position.y, Player.transform.position.z + 11);
                            }
                            else
                            {
                                GameObject BossAttack4 = Instantiate(BossAttack1Prefab);
                                BossAttack4.transform.position = new Vector3(0, 1.8f, Player.transform.position.z + 11);
                            }
                            break;
                        case 2:
                            int num4 = Random.Range(1, 11);
                            GameObject BossAttack5 = Instantiate(BossAttack2Prefab);
                            if (num4 <= 6)
                            {
                                BossAttack5.transform.position = new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z + 11);
                            }
                            else
                            {
                                int offsetX = Random.Range(-1, 2);
                                BossAttack5.transform.position = new Vector3(2 * offsetX, this.transform.position.y, Player.transform.position.z + 11);
                            }
                            break;
                        case 3:
                            int offsetX4 = Random.Range(-1, 2);
                            GameObject BossAttack6 = Instantiate(BossAttack3Prefab);
                            BossAttack6.transform.position = new Vector3(1.8f * offsetX4, this.transform.position.y, Player.transform.position.z + 11);
                            break;
                    }
                    break;
            }

        }

        this.isAttack = false;

    }

    private void Defeat()
    {
        if (Setting.Type == BossSetting.BossType.BossDragon)
        {
            this.Score.GetComponent<ScoreController>().defeatCount += 20;
        }
        else
        {
            this.Score.GetComponent<ScoreController>().defeatCount += 10;
        }
        this.gameDirector.GetComponent<GameDirector>().isStageClear = true;
        Destroy(this.gameObject);
    }

}
