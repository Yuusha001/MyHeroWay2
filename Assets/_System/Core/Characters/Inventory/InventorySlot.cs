using NaughtyAttributes;
using System.Collections.Generic;

namespace MyHeroWay
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData ItemData;
        public ItemDataSO ItemDataSO;
        public InventorySlot(ItemDataSO data)
        {
            this.ItemDataSO = data;
            this.ItemData = new ItemData();
            this.ItemData.itemID = data.id; 
            this.ItemData.inventoryID = DataManager.Instance.data.inventoryData.idItemDefine;
        }
    }

    [System.Serializable]
    public class InventoryColection
    {
        public List<InventorySlot> InventorySlots;

        public InventoryColection()
        {
            InventorySlots = new List<InventorySlot>();
        }
    }
}
