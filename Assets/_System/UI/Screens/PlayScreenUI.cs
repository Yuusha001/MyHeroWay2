using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class PlayScreenUI : ScreenUI
    {
        #region Controller
        [BoxGroup("Controller")]
        public Button primaryBtn;
        [BoxGroup("Controller")]
        public Button secondaryBtn;
        [BoxGroup("Controller")]
        public Button dashBtn;
        [BoxGroup("Controller")]
        public Button hpBtn;
        [BoxGroup("Controller")]
        public Button mpBtn;
        #endregion

        #region Top Bar
        [BoxGroup("Top Bar")]
        public Button settingBtn;
        [BoxGroup("Top Bar")]
        public Button itemBtn;
        [BoxGroup("Top Bar")]
        public Button questBtn;
        #endregion

        #region Level Bar
        /*[BoxGroup("Level Bar")]
        public Button settingBtn;
        [BoxGroup("Level Bar")]
        public Button itemBtn;
        [BoxGroup("Level Bar")]
        public Button questBtn;*/
        #endregion
        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            primaryBtn.onClick.AddListener(PrimaryAttackHandler);
            secondaryBtn.onClick.AddListener(SecondaryAttackHandler);
            dashBtn.onClick.AddListener(DashHandler);
            hpBtn.onClick.AddListener(UseHPHandler);
            mpBtn.onClick.AddListener(UseMPHandler);

            settingBtn.onClick.AddListener(SettingHandler);
            itemBtn.onClick.AddListener(ItemBagHandler);
            questBtn.onClick.AddListener(QuestHandler);
        }

        private void QuestHandler()
        {
            throw new NotImplementedException();
        }

        private void ItemBagHandler()
        {
            throw new NotImplementedException();
        }

        private void SettingHandler()
        {
            PopupManager.Instance.ShowPopup<SettingPopup>();
        }

        private void UseHPHandler()
        {
            throw new NotImplementedException();
        }

        private void UseMPHandler()
        {
            throw new NotImplementedException();
        }

        private void DashHandler()
        {
            PlayerControlManager.Instance.playerController.HandleDash();
        }

        private void SecondaryAttackHandler()
        {
            PlayerControlManager.Instance.playerController.HandleSecondaryAttack();
        }

        private void PrimaryAttackHandler()
        {
            PlayerControlManager.Instance.playerController.HandlePrimaryAttack();
        }
    }
}