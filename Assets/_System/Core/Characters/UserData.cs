
namespace MyHeroWay
{
    [System.Serializable]
    public class UserData
    {
        public InventoryData inventoryData;
        public MapData mapData;
        public int userLevel;
        public int userEXP;

        public UserData()
        {
            inventoryData = new InventoryData();
            mapData = new MapData();
            userLevel = 1;
            userEXP = 0;
        }
    }
}
