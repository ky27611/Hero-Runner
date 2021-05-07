using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Setting")]
public class BossSetting : ScriptableObject
{
    public enum BossType
    {
        BossSlime,
    }

    public BossType Type;
    public string Name;
    public int Hp;
    public int Atk;
    public float Spd;
    public int Exp;

}
