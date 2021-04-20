using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove
{
    protected EnemySetting m_Setting;

     //前方向の速度
    public float velocityZ;
    //横方向の速度
    public float velocityX;

    public bool isMoveX;

    //上方向の速度
    public float velocityY;
    //横方向の移動量
    public float setpositionX = 2f;
    //左右の移動できる範囲
    public float movableRange = 2f;

    public EnemyMove(EnemySetting setting)
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

public class SlimeMove : EnemyMove
{
    public override void Initialize()
    {
       isMoveX = true;
    }

    public override void OnUpdate()
    {
        //myTransform = new Vector3();

        if(isMoveX == true)
        {
            velocityX = 10 * m_Setting.Spd;
        }
        else if (isMoveX == false)
        {
            velocityX = -10 * m_Setting.Spd;
        }

        
        if(isMoveX == true)
        {
            isMoveX = false;
        }
        else if (isMoveX == false)
        {
            isMoveX = true;
        }
       

        m_Setting.myRigidbody.velocity = new Vector3(velocityX, 0, 0);
    }

    public override void Release()
    {
       
    }

    public SlimeMove(EnemySetting setting) : base(setting)
    {
    }
}

public class TurtleMove : EnemyMove
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

    public TurtleMove(EnemySetting setting) : base(setting)
    {
    }
}

public class PurpleMove : EnemyMove
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

    public PurpleMove(EnemySetting setting) : base(setting)
    {
    }
}

