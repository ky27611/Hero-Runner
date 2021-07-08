using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove
{
    protected BossSetting m_Setting;

    /*
    //前方向の速度
    public float velocityZ;
    //横方向の速度
    public float velocityX;
    //上方向の速度
    public float velocityY;

    public bool isMove;

    public bool isMoveZ;
    public bool isMoveX;
    public bool isMoveY;


    //横方向の移動量
    public float setpositionX = 2f;
    //左右の移動できる範囲
    public float movableRange = 2f;

    //移動させるコンポーネントを入れる
    public Rigidbody myRigidbody;

    public float IdleTime;
    */


    public BossMove(BossSetting setting)
    {
        m_Setting = setting;
        //myRigidbody = rigidbody;
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

public class BossSlimeMove : BossMove
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

    public BossSlimeMove(BossSetting setting) : base(setting)
    {
    }

}

public class BossTurtleMove : BossMove
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

    public BossTurtleMove(BossSetting setting) : base(setting)
    {
    }

}

public class BossPurpleMove : BossMove
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

    public BossPurpleMove(BossSetting setting) : base(setting)
    {
    }

}

public class BossDragonMove : BossMove
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

    public BossDragonMove(BossSetting setting) : base(setting)
    {
    }

}