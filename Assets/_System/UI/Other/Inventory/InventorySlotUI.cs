using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class InventorySlotUI : MonoBehaviour, IPointerDownHandler
    {
        private InventoryPopup inventoryPopup;

        public bool isEquipmentSlot;
        public Image icon;
        public GameObject equip;
        private InventorySlot inventory;

        public InventorySlot Inventory { get => inventory; private set => inventory = value; }
        public EquipmentDataSO EquipmentDataSO { get; private set; }
        public EquipmentData EquipmentData { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            ShowInfo();
        }

        public void Initialize(InventoryPopup inventoryPopup, InventorySlot inventorySlot)
        {
            this.inventoryPopup = inventoryPopup;
            this.Inventory = inventorySlot;
            icon.sprite = Inventory.ItemDataSO.icon;
            equip.gameObject.SetActive(isEquipmentSlot);
            EquipmentDataSO = inventory.ItemDataSO as EquipmentDataSO;
            EquipmentData = inventory.ItemData as EquipmentData;
        }

        private void ShowInfo()
        {
            inventoryPopup.equipmentInfoUI.ShowInfo(this);
            inventoryPopup.equipmentUpgradeUI.SetInfo(this);
        }

        public async UniTask EnchanceAsync(int totalExp, System.Action callback)
        {
            int nextExp = DataManager.Instance.equipmentExpContainer.GetWeaponExp(EquipmentDataSO.equipmentRarity).GetEXP(EquipmentData.level + 1);
            EquipmentData.Enchance(totalExp, nextExp);
            while (true)
            {
                nextExp = DataManager.Instance.equipmentExpContainer.GetWeaponExp(EquipmentDataSO.equipmentRarity).GetEXP(EquipmentData.level + 1);
                if (EquipmentData.CanLimitBreak())
                {
                    DataManager.Instance.SaveData();
                    callback?.Invoke();
                    EquipmentData.exp = 0;
                    break;
                }
                
                if (EquipmentData.exp > nextExp)
                {
                    EquipmentData.Enchance(0, nextExp);
                    DataManager.Instance.SaveData();
                    callback?.Invoke();
                }
                else
                {
                    break;
                }
                await UniTask.WaitForSeconds(0.2f);
            }
        }
    }
}
