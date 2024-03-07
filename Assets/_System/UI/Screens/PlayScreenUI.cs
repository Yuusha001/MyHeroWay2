using MyHeroWay.Skills;
using NaughtyAttributes;
using System;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class PlayScreenUI : ScreenUI
    {
        #region Controller
        [BoxGroup("Controller")]
        public SkillBtn[] controllerBtns;
        #endregion

        #region Top Bar
        [BoxGroup("Top Bar")]
        public Button settingBtn;
        [BoxGroup("Top Bar")]
        public Button itemBtn;
        [BoxGroup("Top Bar")]
        public Button questBtn;
        [BoxGroup("Top Bar")]
        public FillBar hpBar;
        [BoxGroup("Top Bar")]
        public FillBar mpBar;
        #endregion

        #region Level Bar
        [BoxGroup("Level Bar")]
        public ExpOrbBar levelBar;
        #endregion

        public SkillsControllerUI skillsControllerUI;
        private Combat playerCombat;
        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            settingBtn.onClick.AddListener(SettingHandler);
            itemBtn.onClick.AddListener(ItemBagHandler);
            questBtn.onClick.AddListener(QuestHandler);
            skillsControllerUI.Initialize(uiManager);
            playerCombat = PlayerControlManager.playerCombat;
            playerCombat.OnStatsChange += StatsChangeHandler;
            hpBar.InitializeBar(false);
            mpBar.InitializeBar(false);
            hpBar.ShowStatusText((int)playerCombat.runtimeStats.health, (int)playerCombat.originalStats.health);
            mpBar.ShowStatusText((int)playerCombat.runtimeStats.mana, (int)playerCombat.originalStats.mana);
            levelBar.InitializeBar();
            DataManager.OnLevelEXP += GainExpHandler;
            SetupController();

        }


        private void SetupController()
        {
            var equipment = PlayerControlManager.Instance.playerController.characterEquipment;

            foreach (var item in controllerBtns)
            {
                SetupSkill(item);
            }
            equipment.SetupPrimaryWeaponGUI(controllerBtns[0]);
            equipment.SetupSecondaryWeaponGUI(controllerBtns[1]);

        }

        private void SetupSkill(SkillBtn skillBtn)
        {
            var skillTree = PlayerControlManager.Instance.playerController.playerSkillTree;
            if (skillTree.Have(skillBtn.skill))
            {
                Skill skill = skillTree.GetSkill<Skill>();
                skill.SetupGUI(skillBtn);
            }
            skillBtn.OnCoolDown(false);
        }

        private void GainExpHandler()
        {
            levelBar?.UpdateStatusText();
            levelBar?.UpdateFillBar();
        }

        private void StatsChangeHandler()
        {
            hpBar?.UpdateFillBar(playerCombat.NormalizeHealth());
            hpBar?.ShowStatusText((int)playerCombat.runtimeStats.health, (int)playerCombat.originalStats.health);

            mpBar?.UpdateFillBar(playerCombat.NormalizeMana());
            mpBar?.ShowStatusText((int)playerCombat.runtimeStats.mana, (int)playerCombat.originalStats.mana);

        }

        private void QuestHandler()
        {
            throw new NotImplementedException();
        }

        private void ItemBagHandler()
        {
            PopupManager.Instance.ShowPopup<InventoryPopup>();
        }

        private void SettingHandler()
        {
            PopupManager.Instance.ShowPopup<SettingPopup>();
        }
    }
}