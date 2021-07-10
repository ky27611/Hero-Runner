using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove
{
    protected EnemySetting m_Setting;

    public bool isMove;

    public float IdleTime;

    public Transform myTramsform;

    public EnemyMove(EnemySetting setting, Transform transform)
    {
        m_Setting = setting;
        myTramsform = transform;
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

/*
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
*/

/*
public class DefeatedMove : EnemyMove
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

    public DefeatedMove(EnemySetting setting, Rigidbody rigidbody) : base(setting, rigidbody)
    {
    }

}
*/

public class HorizontalMove : EnemyMove
{
    private float Direction;
    private float Destination;
    private float Move;

    public override void Initialize()
    {
        IdleTime = 0;
        isMove = true;
        this.Direction = Random.Range(0, 2);
        this.Move = ((1 - this.Direction * 2)) * 0.1f;
    }

    public override void OnUpdate()
    {
        this.IdleTime += Time.deltaTime;

        if (isMove == true)
        {
            if (myTramsform.transform.position.x >= 2.1f || myTramsform.transform.position.x <= -2.1f)
            {
                this.Move *= -1;
            }

            myTramsform.transform.position = new Vector3(myTramsform.position.x + this.Move, myTramsform.position.y, myTramsform.position.z);

            if (IdleTime >= 1)
            {
                isMove = false;
                IdleTime = 0;
            }

        }
        else
        {
            if (IdleTime >= 1)
            {
                isMove = true;
                IdleTime = 0;
            }
        }

    }

    public override void Release()
    {
       
    }

    public HorizontalMove(EnemySetting setting, Transform transform) : base(setting, transform)
    {
    }

}

public class VerticalMove : EnemyMove
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

    public VerticalMove(EnemySetting setting, Transform transform) : base(setting, transform)
    {
    }
}

public class DepthMove : EnemyMove
{
    public override void Initialize()
    {
        
    }

    public override void OnUpdate()
    {
        /*
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
        */
    }

    public override void Release()
    {

    }

    public DepthMove(EnemySetting setting, Transform transform) : base(setting, transform)
    {
    }
}


