using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class EquipmentInfoUI : MonoBehaviour
    {
        private InventoryPopup inventoryPopup;
        public Text title;

        public void Initialize(InventoryPopup _inventoryPopup)
        {
            this.inventoryPopup = _inventoryPopup;
        }

        
    }
}
