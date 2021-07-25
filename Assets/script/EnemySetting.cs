using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Setting")]
public class EnemySetting : ScriptableObject
{

    public enum EnemyType
    {
        Slime,
        Slime2,
        Turtle,
        Turtle2,
        Purple,
        Purple2,
    }

    public EnemyType Type;
    public string Name;
    public int Hp;
    public int Atk;
    public float Spd;
    public int Exp;
    //public Transform myTransform;

}
