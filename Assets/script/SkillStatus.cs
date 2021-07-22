using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Status")]
public class SkillStatus : ScriptableObject
{
    public bool isSkill;

    public float SkillTime;
    public float SkillWaitTime;
    public float SkillRecoveryTime;
}
