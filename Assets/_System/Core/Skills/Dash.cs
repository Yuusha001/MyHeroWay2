using UnityEngine.UI;
using Utils;

namespace MyHeroWay.Skills
{
    public class Dash : Skill
    {
        public override void Initilize(Controller controller)
        {
            base.Initilize(controller);
            this.SkillType = ESkillType.Active;
            this.SkillName = ESkill.Dash;
        }

        public override async void UseSkill(System.Action callBack)
        {
            controller.GetMovement().movementSpeed *= 4;
            await Delay.DoAction(() =>
            {
                controller.GetMovement().movementSpeed /= 4;
            }, 0.3f);
            callBack?.Invoke();
        }

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        public override void SetupGUI(SkillBtn skillBtn)
        {
            this.skillBtn = skillBtn;
            var data = DataManager.Instance.skillDictionary.GetSkillData(SkillType).GetData(SkillName);
            skillBtn.icon.sprite = data.skillIcon;
            skillBtn.coolDown.text = coolDownTimer.ToString();
            skillBtn.fill.fillAmount = coolDownTimer / coolDown;
            skillBtn.value.gameObject.SetActive(false);
        }

        public override void UpdateGUI(SkillBtn skillBtn)
        {
            skillBtn.OnCoolDown(true);
            skillBtn.coolDown.text = coolDownTimer.ToString("0.00");
            skillBtn.fill.fillAmount = coolDownTimer / coolDown;
        }
    }
}
