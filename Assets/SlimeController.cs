using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public enum MoveType 
    {
        Idle,
        Slime,
    }

    private SlimeSetting Setting;

    private Dictionary<MoveType, EnemyMove> m_MoveMap = new Dictionary<MoveType, EnemyMove>();

    private EnemyMove m_CurrentMove;


    // Start is called before the first frame update
    void Start()
    {

        Setting = new SlimeSetting();

        m_MoveMap.Add(MoveType.Idle, new EnemyMove(Setting));
        m_MoveMap.Add(MoveType.Slime, new SlimeMove(Setting));

        m_CurrentMove = m_MoveMap[MoveType.Slime];
        m_CurrentMove.Initialize();

        Setting.myRigidbody = GetComponent<Rigidbody>();
        
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
