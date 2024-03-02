using MyHeroWay.Skills;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillDictionary", menuName = "Skill/SkillDictionary", order = 0)]
public class SkillDictionary : ScriptableObject
{
    public SkillType[] data;

    public SkillDataContainer GetSkillData(ESkillType skillType)
    {
        return data.FirstOrDefault(e => e.skillType == skillType).skillDataContainer;
    }
}

[System.Serializable]
public class SkillType
{
    public ESkillType skillType;
    public SkillDataContainer skillDataContainer;
}
