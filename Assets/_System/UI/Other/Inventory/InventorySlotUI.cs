using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class InventorySlotUI : MonoBehaviour, IPointerDownHandler
    {
        public bool isEquipmentSlot;
        public Image icon;
        public GameObject equip;    
        private InventorySlot inventory;

        public InventorySlot Inventory { get => inventory; private set => inventory = value; }
        public EquipmentDataSO EquipmentData { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            ShowInfo();
        }

        public void Initialize(InventorySlot inventorySlot)
        {
            this.Inventory = inventorySlot;
            icon.sprite = Inventory.ItemDataSO.icon;
            equip.gameObject.SetActive(isEquipmentSlot);
            EquipmentData = inventory.ItemDataSO as EquipmentDataSO;
        }

        private void ShowInfo()
        {

        }
    }
}
