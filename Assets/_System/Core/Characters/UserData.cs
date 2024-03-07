
namespace MyHeroWay
{
    [System.Serializable]
    public class UserData
    {
        public InventoryData inventoryData;
        public int userLevel;
        public int userEXP;

        public UserData()
        {
            inventoryData = new InventoryData();
            userLevel = 1;
            userEXP = 0;
        }
    }
}
