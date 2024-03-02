using MyHeroWay.Skills;
using System;
using UnityEditor;
using UnityEngine.UI;

namespace MyHeroWay
{
    [Serializable]
    public class SkillBtn : Button
    {
        public ESkill skill;
        public Image icon;
        public Image fill;
        public Text coolDown;
        public Text value;


        public void OnCoolDown(bool status)
        {
            coolDown.gameObject.SetActive(status);
            fill.gameObject.SetActive(status);
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(SkillBtn))]
    public class SkillBtnEditor : UnityEditor.UI.ButtonEditor
    {
        public override void OnInspectorGUI()
        {

            SkillBtn component = (SkillBtn)target;

            base.OnInspectorGUI();
            component.skill = (ESkill)EditorGUILayout.EnumPopup("Skill", component.skill);
            component.icon = (Image)EditorGUILayout.ObjectField("Icon", component.icon, typeof(Image), true);
            component.fill = (Image)EditorGUILayout.ObjectField("Fill", component.fill, typeof(Image), true);
            component.coolDown = (Text)EditorGUILayout.ObjectField("Cool Down Text", component.coolDown, typeof(Text), true);
            component.value = (Text)EditorGUILayout.ObjectField("Value", component.value, typeof(Text), true);

        }
    }
#endif
}