
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay.Skills
{
    [System.Serializable]
    public class SkillTree: MonoBehaviour
    {
        public  List<Skill> skillList;

        public void Initialize(Controller controller)
        {
            SetupPlayerBaseSkill();
            foreach (var item in skillList)
            {
                item.Initilize(controller);
                var data = DataManager.Instance.skillDictionary.GetSkillData(item.SkillType).GetData(item.SkillName);
                item.Setup(data);
            }
        }


        public bool Have<T>()
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i] is T)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Have(ESkill skill)
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i].SkillName == skill)
                {
                    return true;
                }
            }
            return false;
        }

        public T GetSkill<T>()
        {
            T popup = default;
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i] is T)
                {
                    popup = skillList[i].GetComponent<T>();
                }
            }
            return popup;
        }

        public void SetupPlayerBaseSkill()
        {
            skillList = new List<Skill>();
            Dash dash = gameObject.AddComponent<Dash>();
            skillList.Add(dash);
        }

        public void UpdateLogic()
        {
            foreach (var item in skillList)
            {
                item.UpdateLogic();
            }
        }
    }
}
