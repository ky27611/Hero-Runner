using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerSetting m_Setting;

    public PlayerState(PlayerSetting setting)
    {
        m_Setting = setting;
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
        m_Setting.myAnimator.SetBool("Jump", true);
        var velo = m_Setting.myRigidbody.velocity;
        velo.y = m_Setting.velocityY;
        m_Setting.myRigidbody.velocity = velo;
    }

    public override void OnUpdate()
    {
        Debug.Log("Jump");
    }

    public override void Release()
    {
        m_Setting.myAnimator.SetBool("Jump", false);
    }

    public PlayerStateJump(PlayerSetting setting) : base(setting)
    {
    }
}

public class PlayerStateSliding : PlayerState
{
    public override void Initialize()
    {
        m_Setting.myAnimator.SetBool("Slide", true);
        m_Setting.myCollider.center = new Vector3(0, 0.35f, 0);
        m_Setting.myCollider.height = 0.3f;
        m_Setting.slidedelta += Time.deltaTime;
    }

    public override void OnUpdate()
    {
        if (m_Setting.slidedelta > 0)
        {
            m_Setting.slidedelta += Time.deltaTime;
            if (m_Setting.slidedelta > m_Setting.slidespan)
            {
                m_Setting.myCollider.center = new Vector3(0, 0.8f, 0);
                m_Setting.myCollider.height = 1.5f;
                m_Setting.slidedelta = 0;
            }
        }
    }

    public override void Release()
    {
        m_Setting.myAnimator.SetBool("Slide", false);
    }

    public PlayerStateSliding(PlayerSetting setting) : base(setting)
    {
    }
}

public class PlayerStateAttack : PlayerState
{
    public override void Initialize()
    {
        m_Setting.atkCollider.enabled = true;
        m_Setting.atkdelta = 0.0f;
    }

    public override void OnUpdate()
    {
        m_Setting.atkdelta += Time.deltaTime;
    }

    public override void Release()
    {
        m_Setting.atkCollider.enabled = false;
        m_Setting.atkdelta = 0;
    }

    public PlayerStateAttack(PlayerSetting setting) : base(setting)
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

    public PlayerStateDamage(PlayerSetting setting) : base(setting)
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
        Debug.Log("Death");
    }

    public override void OnUpdate()
    {
        m_Setting.velocityZ *= m_Setting.coefficient;
        m_Setting.velocityX *= m_Setting.coefficient;
        m_Setting.velocityY *= m_Setting.coefficient;
        m_Setting.myAnimator.speed *= m_Setting.coefficient;
    }

    public override void Release()
    {

    }

    public PlayerStateDeath(PlayerSetting setting) : base(setting)
    {
    }
}
