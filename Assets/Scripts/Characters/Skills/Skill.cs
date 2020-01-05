using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {
    protected SkillType type;
    public int skillLevel = 1;
    public float power;
}

public enum SkillType
{
    healing, attacking, debuffing
};
