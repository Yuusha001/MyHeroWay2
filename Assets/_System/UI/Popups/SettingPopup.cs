using System;
using System.Collections;
using System.Collections.Generic;
using Ultis;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class SettingPopup : PopupUI
    {
        [SerializeField]
        private ToggleBtn musicBtn;
        [SerializeField]
        private ToggleBtn sfxBtn;
        [SerializeField]
        private ToggleBtn vibrateBtn;
        [SerializeField]
        private Button closeBtn;
        [SerializeField]
        private Button creditBtn;
        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager);
            musicBtn.Initialize(AudioManager.MusicSetting == 1, MusicHandler);
            sfxBtn.Initialize(AudioManager.SoundSetting == 1, SFXHandler);
            vibrateBtn.Initialize(AudioManager.VibrateSetting == 1, VibrateHandler);
            closeBtn.onClick.AddListener(Close);
            creditBtn.onClick.AddListener(CreditHandler);
        }

        private void VibrateHandler()
        {
            throw new NotImplementedException();
        }

        private void MusicHandler()
        {
            if (musicBtn == null) return;
            AudioManager.Instance.MusicHandler();
        }

        private void SFXHandler()
        {
            if (sfxBtn == null) return;
            AudioManager.Instance.SFXHandler();
        }

        private void CreditHandler()
        {
            if(vibrateBtn == null) return;
            AudioManager.Instance.VibrateHandler();

        }
    }
}