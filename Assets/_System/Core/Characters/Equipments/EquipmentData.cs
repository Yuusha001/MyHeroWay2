namespace MyHeroWay
{
    [System.Serializable]
    public class EquipmentData
    {
        public EquipmentData(int itemID, int grade = 1)
        {
            this.itemID = itemID;
            this.idInventory = -1;
            this.level = 1;
            this.gemChoice = 0;
            this.grade = grade;
            this.isGemChosen = false;
        }
        public EquipmentData(EquipmentData data)
        {
            this.itemID = data.itemID;
            this.idInventory = data.idInventory;
            this.level = data.level;
            this.gemChoice = data.gemChoice;
            this.grade = data.grade;
            this.isGemChosen = data.isGemChosen;
        }
        public int idInventory;
        public int itemID;
        public int level;
        public int grade;
        public int gemChoice;
        public bool isGemChosen;
    }
}
