using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Setting")]
public class EnemySetting : ScriptableObject
{

    public enum EnemyType
    {
        Slime,
        Turtle,
        Purple
    }

    public EnemyType Type;
    public string Name;
    public int Hp;
    public int Atk;
    public float Spd;
    public int Exp;

    public Rigidbody myRigidbody;

}
