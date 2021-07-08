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
    public float BossAtk;

    private GameObject Score;
    private GameObject gameDirector;
    private GameObject Player;

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

        this.BossHP = Setting.Hp;
        this.BossAtk = Setting.Atk;

        this.Score = GameObject.Find("ScoreDirector");
        this.gameDirector = GameObject.Find("GameDirector");
        this.Player = GameObject.Find("Player");

        this.transform.position = new Vector3(0, this.transform.position.y, Player.transform.position.z + 12);

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
        this.transform.position = new Vector3(0, this.transform.position.y, Player.transform.position.z + 12);
        m_CurrentMove.OnUpdate();

        if (this.BossHP <= 0)
        {
            /*
            this.Score.GetComponent<ScoreController>().DefeatBoss();
            this.gameDirector.GetComponent<GameDirector>().isClear = true;
            Destroy(this.gameObject);
            */

            //this.GetComponent<Renderer>().enabled = false;
            this.gameDirector.GetComponent<GameDirector>().index = GameDirector.Index.StageClear;
            Destroy(this.gameObject);
        }
    }
}
