using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum MoveType 
    {
        Idle,
        Slime,
    }
    
    public EnemySetting Setting; // 設定をアセット化してInspectorから設定してあげる！
    private Dictionary<MoveType, EnemyMove> m_MoveMap = new Dictionary<MoveType, EnemyMove>();

    private EnemyMove m_CurrentMove;


    // Start is called before the first frame update
    void Start() {
        m_MoveMap.Add(MoveType.Idle, new EnemyMove(Setting));
        m_MoveMap.Add(MoveType.Slime, new SlimeMove(Setting));
        Setting.myRigidbody = GetComponent<Rigidbody>();
        
        
        m_CurrentMove = m_MoveMap[MoveType.Slime];
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
