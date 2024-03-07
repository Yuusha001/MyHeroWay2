using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class InventoryStackSlotUI : MonoBehaviour, IPointerDownHandler
    {
        public Image icon;
        public Text stack;
        private InventoryStackSlot inventory;

        public void OnPointerDown(PointerEventData eventData)
        {
            ShowInfo();
        }

        public void Initialize(InventorySlot inventorySlot)
        {
            this.inventory = inventorySlot as InventoryStackSlot;
            icon.sprite = inventory.ItemDataSO.icon;
            stack.text = inventory.stackSize.ToString();
        }

        private void ShowInfo()
        {

        }
    }
}
