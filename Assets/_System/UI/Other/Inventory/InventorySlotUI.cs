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

        public void Initialize(InventorySlot inventorySlot)
        {
            this.Inventory = inventorySlot;
            icon.sprite = Inventory.ItemDataSO.icon;
            equip.gameObject.SetActive(isEquipmentSlot);
            EquipmentDataSO = inventory.ItemDataSO as EquipmentDataSO;
            EquipmentData = inventory.ItemData as EquipmentData;
        }

        private void ShowInfo()
        {
            PopupManager.Instance.GetPopup<InventoryPopup>().equipmentInfoUI.ShowInfo(this);
        }

        public async UniTask EnchanceAsync(int totalExp, System.Action callback)
        {
            int nextExp = await Utils.Delay.DoAction(() =>
                DataManager.Instance.equipmentExpContainer.GetWeaponExp(EquipmentDataSO.equipmentRarity).GetEXP(EquipmentData.level + 1)
            );
            EquipmentData.Enchance(totalExp, nextExp);
            DataManager.Instance.SaveData();
            callback?.Invoke();
        }
    }
}
