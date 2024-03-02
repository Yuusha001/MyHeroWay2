using MyHeroWay.Skills;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/SkillData", order = 1)]
[System.Serializable]
public class SkillData : ScriptableObject
{
    public ESkill skillName;
    public ESkillType skillTpye;
    [ShowAssetPreview]
    public Sprite skillIcon;
    [ResizableTextArea]
    public string skillDescription;
    public float skillCoolDown;
}
