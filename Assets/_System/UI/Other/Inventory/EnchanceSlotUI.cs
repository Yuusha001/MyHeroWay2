using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class EnchanceSlotUI : MonoBehaviour
    {
        private EquipmentUpgradeUI upgradeUI;
        public Button button;
        public Image icon;
        public Text stack;
        public GameObject item;
        public void Initialize(EquipmentUpgradeUI equipmentUpgradeUI)
        {
            upgradeUI = equipmentUpgradeUI;
            button.onClick.AddListener(() => upgradeUI.ShowMaterials());
        }

        public void Setup(MaterialData materialData, ItemDataSO itemDataSO)
        {
            item.SetActive(true);
            icon.sprite = itemDataSO.icon;
            stack.text = materialData.stackSize.ToString();
        }

        public void Remove()
        {
            item.SetActive(false);
            icon.sprite = null;
            stack.text = "";
        }
    }
}
