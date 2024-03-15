using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class InventoryPopup : PopupUI
    {
        public BagUI bag;
        public EquipingUI equipingUI;
        public EquipmentInfoUI equipmentInfoUI;
        public EquipmentUpgradeUI equipmentUpgradeUI;
        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager, onClosed);
            bag.Initialize(this);
            equipmentInfoUI.Initialize(this);
            equipmentUpgradeUI.Initialize(this);
        }
        public override void Show()
        {
            base.Show();
            _popupManager.ShowPopup<TopbarPopup>().title.text = "Inventory";
            _popupManager.ShowPopup<NavigationPopup>();
            equipmentInfoUI.Hide();

        }
    }
}
