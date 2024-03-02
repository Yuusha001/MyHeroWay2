using System;
using UnityEngine;

namespace MyHeroWay.Skills
{
    [System.Serializable]
    public abstract class Skill : MonoBehaviour
    {
        [SerializeField]
        protected float coolDown;
        protected float coolDownTimer;
        private ESkillType skillType;
        private ESkill skillName;
        protected Controller controller;
        protected SkillBtn skillBtn;
        public ESkillType SkillType { get => skillType; protected set => skillType = value; }
        public ESkill SkillName { get => skillName; protected set => skillName = value; }

        public virtual void Initilize(Controller controller)
        {
            this.controller = controller;
        }

        public void Setup(SkillData skillData)
        {
            this.coolDown = skillData.skillCoolDown;
        }

        public virtual void UpdateLogic()
        {
            if (coolDownTimer > 0)
                coolDownTimer -= Time.deltaTime;
            if (skillBtn != null)
            {
                UpdateGUI(skillBtn);
                if(coolDownTimer <= 0)
                {
                    skillBtn.OnCoolDown(false);
                }
            }
        }

        public virtual bool CanUseSkill()
        {
            if (coolDownTimer <= 0)
            {
                coolDownTimer = coolDown;
                return true;
            }
            return false;
        }

        public abstract void UseSkill(Action callBack);
        public abstract void SetupGUI(SkillBtn skillBtn);
        public abstract void UpdateGUI(SkillBtn skillBtn);
    }
}
