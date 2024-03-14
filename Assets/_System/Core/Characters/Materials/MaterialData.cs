namespace MyHeroWay
{
    [System.Serializable]
    public class MaterialData : ItemData
    {
        public int stackSize;
        public MaterialData(int itemID)
        {
            this.itemID = itemID;
            this.stackSize = 1;
        }

        public MaterialData(MaterialData materialData)
        {
            this.itemID = materialData.itemID;
            this.inventoryID = materialData.inventoryID;
            this.stackSize = materialData.stackSize;
        }
        public void AddStack() => stackSize++;
        public void RemoveStack() => stackSize--;

        public void RemoveStack(int stack)
        {
            stackSize -= stack;
        }
    }
}
