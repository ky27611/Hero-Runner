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


    public EnemyMove(EnemySetting setting, Rigidbody rigidbody)
    {
        m_Setting = setting;
        myRigidbody = rigidbody;
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

public class HorizontalMove : EnemyMove
{
    public override void Initialize()
    {
        isMoveX = true;
        IdleTime = 0;
    }

    public override void OnUpdate()
    {

        if (isMove == false)
        {
            this.IdleTime += Time.deltaTime;
            if (IdleTime >= 5)
            {
                isMove = true;
                IdleTime = 0;
            }
        }
        else if (isMove == true)
        {
            if (isMoveX == true)
            {
                velocityX = 1 * m_Setting.Spd;
                isMove = false;
            }
            else if (isMoveX == false)
            {
                velocityX = -1 * m_Setting.Spd;
                isMove = false;
            }
            
            if (isMoveX == true)
            {
                isMoveX = false;
            }
            else if (isMoveX == false)
            {
                isMoveX = true;
            }

            myRigidbody.velocity = new Vector3(velocityX, 0, 0);
            velocityX = 0;
        }

    }

    public override void Release()
    {
       
    }

    public HorizontalMove(EnemySetting setting, Rigidbody rigidbody) : base(setting, rigidbody)
    {
    }

}

public class VerticalMove : EnemyMove
{
    public override void Initialize()
    {
        isMoveY = true;
        IdleTime = 0;
    }

    public override void OnUpdate()
    {
        if (isMove == false)
        {
            this.IdleTime += Time.deltaTime;
            if (IdleTime >= 1)
            {
                isMove = true;
                IdleTime = 0;
            }
        }
        else if (isMove == true)
        {
            if (isMoveY == true)
            {
                velocityY = 0.2f * m_Setting.Spd;
                isMove = false;

            }
            else if (isMoveY == false)
            {
                velocityY = -0.2f * m_Setting.Spd;
                isMove = false;
            }


            if (isMoveY == true)
            {
                isMoveY = false;
            }
            else if (isMoveY == false)
            {
                isMoveY = true;
            }


            myRigidbody.velocity = new Vector3(0, velocityY, 0);
        }
    }

    public override void Release()
    {

    }

    public VerticalMove(EnemySetting setting, Rigidbody rigidbody) : base(setting, rigidbody)
    {
    }
}

public class DepthMove : EnemyMove
{
    public override void Initialize()
    {
        isMoveZ = true;
        IdleTime = 0;
    }

    public override void OnUpdate()
    {
        if (isMove == false)
        {
            this.IdleTime += Time.deltaTime;
            if (IdleTime >= 5)
            {
                isMove = true;
                IdleTime = 0;
            }
        }
        else if (isMove == true)
        {
            if (isMoveZ == true)
            {
                velocityZ = 1 * m_Setting.Spd;
                isMove = false;
            }
            else if (isMoveZ == false)
            {
                velocityZ = -1 * m_Setting.Spd;
                isMove = false;
            }

            if (isMoveZ == true)
            {
                isMoveZ = false;
            }
            else if (isMoveZ == false)
            {
                isMoveZ = true;
            }

            myRigidbody.velocity = new Vector3(0, 0, velocityZ);
            velocityZ = 0;
        }
    }

    public override void Release()
    {

    }

    public DepthMove(EnemySetting setting, Rigidbody rigidbody) : base(setting, rigidbody)
    {
    }
}

