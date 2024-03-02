using MyHeroWay.Skills;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataContainer", menuName = "Skill/SkillDataContainer", order = 2)]
[System.Serializable]
public class SkillDataContainer : ScriptableObject
{
    public SkillData[] skillDatas;
    public SkillData GetData(ESkill name)
    {
       return skillDatas.FirstOrDefault(e => e.skillName == name);
    }
}

