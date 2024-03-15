namespace MyHeroWay
{
    [System.Serializable]
    public class InventoryStackSlot : InventorySlot
    {
        public int stackSize;

        public InventoryStackSlot(ItemDataSO itemDataSO) : base(itemDataSO)
        {
            this.stackSize = 1;
            this.ItemDataSO = itemDataSO;
            this.ItemData.itemID = itemDataSO.id;
            this.ItemData = ItemDataSO.prefab.data;
        }

        public InventoryStackSlot(ItemGame item) : base(item)
        {
            this.stackSize = 1;
            this.ItemDataSO = item.dataSO;
            this.ItemData = item.data;
        }

        public void AddStack() => stackSize++;
        public void RemoveStack() => stackSize--;
    }
}
