using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class EquipmentUpgradeUI : MonoBehaviour
    {
        private InventoryPopup inventoryPopup;
        private InventorySlotUI inventorySlotUI;
        private EquipmentData equipmentData;
        private EquipmentDataSO equipmentDataSO;
        private EquipmentExpSO equipmentExpSO;

        [BoxGroup("Result"), ReadOnly]
        public Button confirmBtn;
        [BoxGroup("Result"), ReadOnly]
        public Text resultTitleTxt;
        public List<InventoryStackSlotUI> enchanceItems;
        public EnchanceSlotUI[] enchanceSlotUIs;
        public void Initialize(InventoryPopup inventoryPopup)
        {
            this.inventoryPopup = inventoryPopup;
            confirmBtn.onClick.AddListener(HandleEnhane);
            enchanceItems = new List<InventoryStackSlotUI>();
            foreach (var item in enchanceSlotUIs)
            {
                item.Initialize(this);
            }
            Hide();
        }

        public void SetInfo(InventorySlotUI inventorySlotUI)
        {
            this.inventorySlotUI = inventorySlotUI;
            equipmentDataSO = inventorySlotUI.EquipmentDataSO;
            equipmentData = inventorySlotUI.EquipmentData;
            equipmentExpSO = DataManager.Instance.equipmentExpContainer.GetWeaponExp(equipmentDataSO.equipmentRarity);
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            this.transform.DOLocalMoveX(400, 0.3f);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
            this.transform.DOLocalMoveX(150, 0.3f);
            enchanceItems.Clear();
            foreach (var item in enchanceSlotUIs)
            {
                item.Remove();
            }
        }

        private async void HandleEnhane()
        {
            if (!equipmentData.CanEnchance()) return;
            int totalEXP = GetExpbyMaterials();
            await inventorySlotUI.EnchanceAsync(totalEXP, () =>
            {
                if (equipmentData.CanLimitBreak())
                {
                    resultTitleTxt.text = "Limit Break";
                }
                else
                {
                    resultTitleTxt.text = "Enchance";
                }
                inventoryPopup.equipmentInfoUI.UpdateLevel();
                inventoryPopup.equipmentInfoUI.UpdateGrades();
                inventoryPopup.equipmentInfoUI.UpdateStats();
            });
        }

        public void ShowMaterials()
        {
            inventoryPopup.bag.SetNavigation(1);
        }

        private void UpdateEnchanceSlot()
        {
            for (int i = 0; i < enchanceItems.Count; i++)
            {
                enchanceSlotUIs[i].Setup(enchanceItems[i].MaterialData, enchanceItems[i].EnhancementItemSO);
            }
        }

        public void AddEnchanceMaterial(InventoryStackSlotUI inventoryStackSlotUI)
        {
            if (!enchanceItems.Contains(inventoryStackSlotUI))
                enchanceItems.Add(inventoryStackSlotUI);
            UpdateEnchanceSlot();
        }


        private int GetExpbyMaterials()
        {
            int currentEXP = equipmentData.exp + equipmentExpSO.GetTotalEXP(equipmentData.level);
            int limitBreakEXP = equipmentExpSO.GetLimitBreakEXP(equipmentData.grade);
            int diffEXP = limitBreakEXP - currentEXP;
            int exp = 0;
            int stack = 0;

            for (int i = 0; i < enchanceItems.Count; i++)
            {
                int itemEXP = enchanceItems[i].EnhancementItemSO.value;
                for (int j = 0; j < enchanceItems[i].MaterialData.stackSize; j++)
                {
                    exp += itemEXP;
                    stack++;
                    if (exp >= diffEXP)
                    {
                        DataManager.Instance.RemoveMaterial(enchanceItems[i].MaterialData, stack);
                        if (enchanceItems[i].MaterialData.stackSize == 0)
                        {
                            InventoryManager.Instance.RemoveInventoryStackUI(enchanceItems[i], inventoryPopup.bag.materialSlots);
                        }
                        InventoryManager.Instance.UpdateItem();
                        UpdateEnchanceSlot();
                        return exp;
                    }
                }
                enchanceSlotUIs[i].Remove();
                DataManager.Instance.RemoveMaterial(enchanceItems[i].MaterialData, stack);
                InventoryManager.Instance.RemoveInventoryStackUI(enchanceItems[i], inventoryPopup.bag.materialSlots);
                enchanceItems.Remove(enchanceItems[i]);
            }

            InventoryManager.Instance.UpdateItem();
            return exp;
        }
    }
}
