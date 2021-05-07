using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public enum BossType
    {
        Idle,
        BossSlime,
    }

    public BossSetting Setting;
    private Dictionary<BossType, BossMove> m_BossMap = new Dictionary<BossType, BossMove>();

    private BossMove m_CurrentMove;

    public float BossHP;
    public float BossAtk;

    private GameObject Score;
    private GameObject gameDirector;

    // Start is called before the first frame update
    void Start()
    {
        m_BossMap.Add(BossType.Idle, new BossMove(Setting, GetComponent<Rigidbody>()));
        m_BossMap.Add(BossType.BossSlime, new BossMove(Setting, GetComponent<Rigidbody>()));

        switch (Setting.Type)
        {
            case BossSetting.BossType.BossSlime:
                ChangeMove(BossType.BossSlime);
                break;
        }

        this.BossHP = Setting.Hp;
        this.BossAtk = Setting.Atk;

        this.Score = GameObject.Find("ScoreDirector");
        this.gameDirector = GameObject.Find("GameDirector");

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
        m_CurrentMove.OnUpdate();

        if (this.BossHP <= 0)
        {
            this.Score.GetComponent<ScoreController>().DefeatBoss();
            this.gameDirector.GetComponent<GameDirector>().isClear = true;
            Destroy(this.gameObject);
        }
    }
}
