using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class TopbarPopup : PopupUI
    {
        [BoxGroup("Diamond")]
        public Text diamondTxt;

        [BoxGroup("Gold")]
        public Text goldTxt;

        public Button closeBtn;
        public Text title;
        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager, onClosed);
            closeBtn.onClick.AddListener(Close);
        }
        public override void Close()
        {
            base.Close();
            _popupManager.GetPopup<InventoryPopup>().Close();
            _popupManager.GetPopup<NavigationPopup>().Close();

        }
    }
}
