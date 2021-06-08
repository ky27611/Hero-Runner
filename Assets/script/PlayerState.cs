using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerSettingData m_Setting;
    protected PlayerSettingComponent m_Compo;

    public PlayerState(PlayerSettingData setting, PlayerSettingComponent compo)
    {
        m_Setting = setting;
        m_Compo = compo;
    }

    public virtual void Initialize()
    {

    }

    public virtual void Release()
    {
        
    }
    
    public virtual void OnUpdate()
    {

    }
}

public class PlayerStateJump : PlayerState
{
    public override void Initialize()
    {
        m_Compo.myAnimator.SetTrigger("JumpTrigger");
        var velo = m_Compo.myRigidbody.velocity;
        velo.y = m_Setting.velocityY;
        m_Compo.myRigidbody.velocity = velo;
    }

    public override void OnUpdate()
    {
        Debug.Log("Jump");
    }

    public override void Release()
    {
        //m_Setting.myAnimator.SetBool("Jump", false);
    }

    public PlayerStateJump(PlayerSettingData setting, PlayerSettingComponent compo) : base(setting, compo)
    {
    }
}

public class PlayerStateSliding : PlayerState
{
    public override void Initialize()
    {
        m_Compo.myAnimator.SetTrigger("SlidingTrigger");
        m_Compo.myCollider.center = new Vector3(0, 0.35f, 0);
        m_Compo.myCollider.height = 0.3f;
        m_Setting.slidedelta += Time.deltaTime;
    }

    public override void OnUpdate()
    {
        if (m_Setting.slidedelta > 0)
        {
            m_Setting.slidedelta += Time.deltaTime;
            if (m_Setting.slidedelta > m_Setting.slidespan)
            {
                m_Compo.myCollider.center = new Vector3(0, 0.8f, 0);
                m_Compo.myCollider.height = 1.5f;
                m_Setting.slidedelta = 0;
            }
        }
    }

    public override void Release()
    {
        //m_Setting.myAnimator.SetBool("Slide", false);
    }

    public PlayerStateSliding(PlayerSettingData setting, PlayerSettingComponent compo) : base(setting, compo)
    {
    }
}

public class PlayerStateAttack : PlayerState
{
    private GameObject Player;
    private Transform AtkCollider;

    public override void Initialize()
    {
        this.Player = GameObject.Find("Player");
        this.AtkCollider = Player.transform.Find("AttackCollider");
        this.AtkCollider.GetComponent<Attack>().isAttack = true;
        m_Compo.myAnimator.SetTrigger("AttackTrigger");
        //m_Setting.isAttack = true;
        m_Setting.atkdelta = 0.0f;
    }

    public override void OnUpdate()
    {
        m_Setting.atkdelta += Time.deltaTime;
    }

    public override void Release()
    {
        this.AtkCollider.GetComponent<Attack>().isAttack = false;
        //m_Setting.isAttack = false;
        m_Setting.atkdelta = 0;
    }

    public PlayerStateAttack(PlayerSettingData setting, PlayerSettingComponent compo) : base(setting, compo)
    {
    }

}

public class PlayerStateDamage : PlayerState
{
    public override void Initialize()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

    public override void Release()
    {
        
    }

    public PlayerStateDamage(PlayerSettingData setting, PlayerSettingComponent compo) : base(setting, compo)
    {
    }
}

public class PlayerStateDeath : PlayerState
{
    private GameObject score;
    private GameObject gamedirector;

    public override void Initialize()
    {
        this.score = GameObject.Find("ScoreDirector");
        this.score.GetComponent<ScoreController>().isTimeScore = false;
        this.gamedirector = GameObject.Find("GameDirector");
        this.gamedirector.GetComponent<GameDirector>().isGameOver = true;
        m_Compo.myAnimator.SetBool("Death", true);
        Debug.Log("Death");
        m_Setting.velocityZ = 0;
        m_Setting.velocityX = 0;
        m_Setting.velocityY = 0;
        
    }

    public override void OnUpdate()
    {
        /*
        m_Setting.velocityZ *= m_Setting.coefficient;
        m_Setting.velocityX *= m_Setting.coefficient;
        m_Setting.velocityY *= m_Setting.coefficient;
        m_Setting.myAnimator.speed *= m_Setting.coefficient;
        */
    }

    public override void Release()
    {
        this.score.GetComponent<ScoreController>().isTimeScore = true;
        this.gamedirector.GetComponent<GameDirector>().isGameOver = false;
        m_Compo.myAnimator.SetBool("Death", false);
        m_Setting.PlayerHP = 1;
        m_Setting.velocityZ = 16;
        m_Setting.velocityX = 12;
        m_Setting.velocityY = 4;
        m_Compo.myAnimator.SetFloat("Speed", 0);
    }

    public PlayerStateDeath(PlayerSettingData setting, PlayerSettingComponent compo) : base(setting, compo)
    {
    }
}

public class PlayerStateBossBattle : PlayerState
{
    private GameObject score;
    private GameObject gamedirector;

    public override void Initialize()
    {
        this.score = GameObject.Find("ScoreDirector");
        this.score.GetComponent<ScoreController>().isTimeScore = false;
        //this.gamedirector = GameObject.Find("GameDirector");
        m_Setting.velocityZ = 0;
        m_Compo.myAnimator.SetFloat("Speed", 0);
    }

    public override void OnUpdate()
    {
        
    }

    public override void Release()
    {

    }

    public PlayerStateBossBattle(PlayerSettingData setting, PlayerSettingComponent compo) : base(setting, compo)
    {
    }
}
