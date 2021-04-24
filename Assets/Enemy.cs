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

    // Start is called before the first frame update
    void Start()
    {
        m_MoveMap.Add(MoveType.Idle, new EnemyMove(Setting));
        m_MoveMap.Add(MoveType.Horizontal, new HorizontalMove(Setting));
        m_MoveMap.Add(MoveType.Vertical, new VerticalMove(Setting));
        m_MoveMap.Add(MoveType.Depth, new DepthMove(Setting));
        Setting.myRigidbody = GetComponent<Rigidbody>();

        
        switch (Setting.Type)
        {
            case EnemyType.Slime:
                ChangeMove(MoveType.Horizontal);
                break;
            
            case EnemyType.Turtle:
                ChangeMove(MoveType.Depth);
                break;
            
            case EnemyType.Purple:
                ChangeMove(MoveType.Verticle);
                break;

        }
        
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
        m_CurrentMove = m_MoveMap[MoveType.Idle];
        m_CurrentMove.Initialize();
    }

    private void ChangeMove(MoveType move)
    {
        m_CurrentMove.Release();
        m_CurrentMove = m_MoveMap[move];
        m_CurrentMove.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentMove.OnUpdate();
    }
    
}
