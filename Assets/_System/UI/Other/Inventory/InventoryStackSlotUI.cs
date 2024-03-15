using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class InventoryStackSlotUI : MonoBehaviour, IPointerDownHandler
    {
        private InventoryPopup inventoryPopup;
        public Image icon;
        public Text stack;
        private InventoryStackSlot inventory;
        public InventoryStackSlot Inventory { get => inventory; private set => inventory = value; }
        public ItemDataSO ItemDataSO { get; private set; }
        public MaterialData MaterialData { get; private set; }
        public EnhancementItemSO EnhancementItemSO { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (inventoryPopup.equipmentUpgradeUI.gameObject.activeSelf && ItemDataSO.itemType == EItemType.Enchancement)
            {
                AddEnchanceMaterial();
            }
            else
            {
                ShowInfo();

            }
        }

        public void Initialize(InventoryPopup inventoryPopup, InventorySlot inventorySlot)
        {
            this.inventoryPopup = inventoryPopup;
            this.Inventory = inventorySlot as InventoryStackSlot;
            icon.sprite = Inventory.ItemDataSO.icon;
            stack.text = Inventory.stackSize.ToString();
            MaterialData = inventory.ItemData as MaterialData;
            ItemDataSO = inventory.ItemDataSO;
            if (ItemDataSO.itemType == EItemType.Enchancement)
            {
                EnhancementItemSO = inventory.ItemDataSO as EnhancementItemSO;
            }
        }

        private void ShowInfo()
        {

        }

        private void AddEnchanceMaterial()
        {
            inventoryPopup.equipmentUpgradeUI.AddEnchanceMaterial(this);
        }
    }
}
