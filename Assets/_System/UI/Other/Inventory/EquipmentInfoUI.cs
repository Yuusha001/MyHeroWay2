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

        [BoxGroup("Result"), ReadOnly]
        public Transform equipmentResult;
        [BoxGroup("Result"), ReadOnly]
        public Button confirmBtn;
        [BoxGroup("Result"), ReadOnly]
        public Text resultTitleTxt;

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
        [BoxGroup("Enchance"), ReadOnly]
        public Button[] enchanceItemBtns;

        public Stack<MaterialData> enchanceItems;

        public void Initialize(InventoryPopup _inventoryPopup)
        {
            this.inventoryPopup = _inventoryPopup;
            enhanceBtn.onClick.AddListener(() =>
            {
                equipmentResult.DOLocalMoveX(400, 0.3f);
            });
            confirmBtn.onClick.AddListener(HandleEnhane);
            foreach (var item in enchanceItemBtns)
            {
                item.onClick.AddListener(() => inventoryPopup.bag.SetNavigation(1));
            }
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

        private async void UpdateLevel()
        {
            levelTxt.text = $"Lv {equipmentData.level}/{equipmentData.grade * 20}";
            int nextExp = await Utils.Delay.DoAction(() =>
                DataManager.Instance.equipmentExpContainer.GetWeaponExp(equipmentDataSO.equipmentRarity).GetEXP(equipmentData.level + 1)
            );
            expbar.fillAmount = equipmentData.exp * 1f / nextExp;
        }


        private void UpdateGrades()
        {
            for (int i = 0; i < grades.Length; i++)
            {
                grades[i].color = i < equipmentData.grade ? Color.white : Color.black;
            }
        }

        private void UpdateStats()
        {
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].color = i < equipmentData.grade ? unlockStats : lockedStats;
            }
        }

        public void AddEnchanceMaterial()
        {

        }

        private async void HandleEnhane()
        {
            if (!equipmentData.CanEnchance()) return;
            int EXP = GetExpbyMaterials();
            await inventorySlotUI.EnchanceAsync(EXP, () =>
            {
                if (equipmentData.CanLimitBreak())
                {
                    resultTitleTxt.text = "Limit Break";
                }
                else
                {
                    resultTitleTxt.text = "Enchance";
                }
            });
            UpdateLevel();
            UpdateGrades();
            UpdateStats();
        }

        private int GetExpbyMaterials()
        {
            int nextExp = DataManager.Instance.equipmentExpContainer.GetWeaponExp(equipmentDataSO.equipmentRarity).GetEXP(equipmentData.level + 1);
            int exp = 0;
            int stack = 0;
            foreach (var item in enchanceItems)
            {
                int itemEXP = DataManager.Instance.itemContainer.GetEnhancementItemObejct(item.itemID).value;
                for (int i = 0; i < item.stackSize; i++)
                {
                    exp += itemEXP;
                    stack++;
                    if (exp > nextExp)
                    {
                        item.RemoveStack(stack);
                        return exp;
                    }
                }
                item.RemoveStack(stack);
            }
            return exp;
        }
    }
}
