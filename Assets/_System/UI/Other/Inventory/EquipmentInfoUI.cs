using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
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
        private InventorySlotUI inventorySlotUI;
        private EquipmentData equipmentData;
        private EquipmentDataSO equipmentDataSO;

        [BoxGroup("Info"), ReadOnly]
        public Button enhanceBtn;
        [BoxGroup("Info"), ReadOnly]
        public Button equipBtn;
        [BoxGroup("Info"), ReadOnly]
        public Image icon;
        [BoxGroup("Info"), ReadOnly]
        public Image element;
        [BoxGroup("Info"), ReadOnly]
        public Image expbar;
        [BoxGroup("Info"), ReadOnly]
        public Image[] grades;
        [BoxGroup("Info"), ReadOnly]
        public Text equipmentNameTxt;
        [BoxGroup("Info"), ReadOnly]
        public Text levelTxt;
        [BoxGroup("Info"), ReadOnly]
        public Text descriptionTxt;
        [BoxGroup("Stats Info"), ReadOnly]
        public Color unlockStats;
        [BoxGroup("Stats Info"), ReadOnly]
        public Color lockedStats;
        [BoxGroup("Stats Info"), ReadOnly]
        public Text[] stats;
        public void Initialize(InventoryPopup _inventoryPopup)
        {
            this.inventoryPopup = _inventoryPopup;
            enhanceBtn.onClick.AddListener(() => inventoryPopup.equipmentUpgradeUI.Show());
            this.gameObject.SetActive(false);

        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void ShowInfo(InventorySlotUI inventorySlotUI)
        {
            this.inventorySlotUI = inventorySlotUI;
            this.gameObject.SetActive(true);
            equipmentDataSO = inventorySlotUI.EquipmentDataSO;
            equipmentData = inventorySlotUI.EquipmentData;
            equipmentNameTxt.text = equipmentDataSO.itemName;
            icon.sprite = equipmentDataSO.icon;
            descriptionTxt.text = equipmentDataSO.description;
            InitializeStats();
            UpdateLevel();
            UpdateGrades();
            UpdateStats();
        }

        private void InitializeStats()
        {
            if (equipmentDataSO.equipmentElementType == EElementType.None)
            {
                element.gameObject.SetActive(false);
            }
            else
            {
                element.gameObject.SetActive(true);
                element.sprite = DataManager.Instance.elementWeaponDictionary.GetSprite(equipmentDataSO.equipmentElementType);
            }

            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].text = equipmentDataSO.gradeSkillDesc[i].ToString();
            }

        }

        public async void UpdateLevel()
        {
            levelTxt.text = $"Lv {equipmentData.level}/{equipmentData.grade * 20}";
            int nextExp = await Utils.Delay.DoAction(() =>
                DataManager.Instance.equipmentExpContainer.GetWeaponExp(equipmentDataSO.equipmentRarity).GetEXP(equipmentData.level + 1)
            );
            expbar.fillAmount = equipmentData.exp * 1f / nextExp;
        }


        public void UpdateGrades()
        {
            for (int i = 0; i < grades.Length; i++)
            {
                grades[i].color = i < equipmentData.grade ? Color.white : Color.black;
            }
        }

        public void UpdateStats()
        {
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].color = i < equipmentData.grade ? unlockStats : lockedStats;
            }
        }
    }
}
