using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class BagUI : MonoBehaviour
    {
        private InventoryPopup inventoryPopup;
        public List<InventorySlotUI> equipmentSlots;
        public List<InventoryStackSlotUI> materialSlots;

        [BoxGroup("Navigation")]
        public Button GearBtn;
        [BoxGroup("Navigation")]
        public Button ItemBtn;
        [BoxGroup("Navigation")]
        public Transform GearParent;
        [BoxGroup("Navigation")]
        public Transform ItemParent;

        [BoxGroup("Equipment")]
        public Button AllBtn;
        [BoxGroup("Equipment")]
        public Button WeaponBtn;
        [BoxGroup("Equipment")]
        public Button HelmetBtn;
        [BoxGroup("Equipment")]
        public Button GauntletBtn;
        [BoxGroup("Equipment")]
        public Button ArmorBtn;
        [BoxGroup("Equipment")]
        public Button AmuletBtn;
        [BoxGroup("Equipment")]
        public Button RingBtn;
        [BoxGroup("Equipment")]
        public Button BootBtn;
        [BoxGroup("Equipment")]
        public Color ActiveColor;
        [BoxGroup("Equipment")]
        public Color InactiveColor;
        [BoxGroup("Equipment")]
        public Transform equipmentHolder;

        [BoxGroup("Material")]
        public Transform materialHolder;

        public void Initialize(InventoryPopup _inventoryPopup)
        {
            this.inventoryPopup = _inventoryPopup;
            AllBtn.onClick.AddListener(() => SelectAll());
            WeaponBtn.onClick.AddListener(() => SelectTypeEquipment(EEquipmentType.WEAPON));
            HelmetBtn.onClick.AddListener(() => SelectTypeEquipment(EEquipmentType.HELMET));
            GauntletBtn.onClick.AddListener(() => SelectTypeEquipment(EEquipmentType.GAUNTLET));
            ArmorBtn.onClick.AddListener(() => SelectTypeEquipment(EEquipmentType.ARMOR));
            AmuletBtn.onClick.AddListener(() => SelectTypeEquipment(EEquipmentType.AMULET));
            RingBtn.onClick.AddListener(() => SelectTypeEquipment(EEquipmentType.RING));
            BootBtn.onClick.AddListener(() => SelectTypeEquipment(EEquipmentType.BOOTS));
            GearBtn.onClick.AddListener(() => SetNavigation(0));
            ItemBtn.onClick.AddListener(() => SetNavigation(1));
            SetNavigation(0);
            SetStatusBtn(EEquipmentType.ALL);
            InventoryManager.Instance.UpdateItemUI += Refresh;
            Spawn();
        }

        public void Refresh()
        {
            
            RefeshEquipments();
            RefreshMaterials();
            /*equippingUI.Refresh();
            upgrade.statsUI.Refresh();*/
        }


        private void Spawn()
        {
            InventoryManager.Instance.SpawnMaterialUI(materialHolder, materialSlots);
            InventoryManager.Instance.SpawnEquipmentUI(equipmentHolder, equipmentSlots);
        }

        private void RefreshMaterials()
        {
            List<InventoryStackSlot> listItems = InventoryManager.Instance.materials;
            for (int index = materialSlots.Count; index < listItems.Count; index++)
            {
                InventoryManager.Instance.AddInventoryStackUI(materialHolder, materialSlots);
            }
            for (int i = 0; i < materialSlots.Count; i++)
            {
                materialSlots[i].Initialize(listItems[i]);
            }
        }

        private void RefeshEquipments()
        {
            List<InventorySlot> listItems = InventoryManager.Instance.equipments;
            for (int index = equipmentSlots.Count; index < listItems.Count; index++)
            {
                InventoryManager.Instance.AddInventoryUI(equipmentHolder, equipmentSlots);
            }
            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                equipmentSlots[i].Initialize(listItems[i]);
                equipmentSlots[i].gameObject.SetActive(i < listItems.Count);
            }
            
        }

        private void SelectTypeEquipment(EEquipmentType equipmentType)
        {
            SetStatusBtn(equipmentType);
            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                if (equipmentSlots[i].Inventory != null)
                {
                    if (equipmentSlots[i].EquipmentDataSO.equipmentType != equipmentType)
                    {
                        equipmentSlots[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        equipmentSlots[i].gameObject.SetActive(true);
                    }
                }
            }
        }

        private void SelectAll()
        {
            SetStatusBtn(EEquipmentType.ALL);

            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                if (equipmentSlots[i].Inventory != null)
                {
                    if (equipmentSlots[i].isEquipmentSlot == true)
                    {
                        equipmentSlots[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        equipmentSlots[i].gameObject.SetActive(true);
                    }
                }
            }
        }

        public void SetNavigation(int num)
        {
            GearBtn.transform.GetChild(0).GetComponent<Image>().DOFade(num == 0 ? 1 : 0, 0.2f);
            ItemBtn.transform.GetChild(0).GetComponent<Image>().DOFade(num == 1 ? 1 : 0, 0.2f);
            GearParent.gameObject.SetActive(num == 0);
            ItemParent.gameObject.SetActive(num == 1);

        }

        private void SetStatusBtn(EEquipmentType equipmentType)
        {
            AllBtn.GetComponent<Image>().color = InactiveColor;
            WeaponBtn.GetComponent<Image>().color = InactiveColor;
            HelmetBtn.GetComponent<Image>().color = InactiveColor;
            ArmorBtn.GetComponent<Image>().color = InactiveColor;
            GauntletBtn.GetComponent<Image>().color = InactiveColor;
            BootBtn.GetComponent<Image>().color = InactiveColor;
            AmuletBtn.GetComponent<Image>().color = InactiveColor;
            RingBtn.GetComponent<Image>().color = InactiveColor;

            switch (equipmentType)
            {
                case EEquipmentType.WEAPON:
                    WeaponBtn.GetComponent<Image>().color = ActiveColor;
                    break;
                case EEquipmentType.ARMOR:
                    ArmorBtn.GetComponent<Image>().color = ActiveColor;
                    break;
                case EEquipmentType.BOOTS:
                    BootBtn.GetComponent<Image>().color = ActiveColor;
                    break;
                case EEquipmentType.RING:
                    RingBtn.GetComponent<Image>().color = ActiveColor;
                    break;
                case EEquipmentType.AMULET:
                    AmuletBtn.GetComponent<Image>().color = ActiveColor;
                    break;
                case EEquipmentType.ALL:
                    AllBtn.GetComponent<Image>().color = ActiveColor;
                    break;
                case EEquipmentType.HELMET:
                    HelmetBtn.GetComponent<Image>().color = ActiveColor;
                    break;
                case EEquipmentType.GAUNTLET:
                    GauntletBtn.GetComponent<Image>().color = ActiveColor;
                    break;
            }
        }
    }
}
