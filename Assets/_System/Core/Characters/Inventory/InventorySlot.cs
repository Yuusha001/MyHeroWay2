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
            this.ItemData = ItemDataSO.prefab.data;
            this.ItemData.itemID = data.id;
        }
    }
}
