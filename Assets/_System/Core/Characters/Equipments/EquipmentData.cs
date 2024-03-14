namespace MyHeroWay
{
    [System.Serializable]
    public class EquipmentData : ItemData
    {
        public int level;
        public int exp;
        public int grade;
        public int gemChoice;
        public bool isGemChosen;

        public EquipmentData(int itemID, int grade = 1)
        {
            this.itemID = itemID;
            this.inventoryID = -1;
            this.level = 1;
            this.exp = 0;
            this.gemChoice = 0;
            this.grade = grade;
            this.isGemChosen = false;
        }
        public EquipmentData(EquipmentData data)
        {
            this.itemID = data.itemID;
            this.inventoryID = data.inventoryID;
            this.level = data.level;
            this.exp = data.exp;
            this.gemChoice = data.gemChoice;
            this.grade = data.grade;
            this.isGemChosen = data.isGemChosen;
        }

        public bool CanEnchance()
        {
            return (this.level < this.grade * 20);
        }

        public void Enchance(int expData, int nextExp)
        {
            if (!CanEnchance()) return;
            int totalExp = this.exp + expData;
            if (totalExp >= nextExp)
            {
                this.exp = totalExp-nextExp;
                this.level++;
                if (level == 80)
                {
                    this.exp = 0;
                }
            }
            else
            {
                this.exp = totalExp;
            }

        }

        public bool CanLimitBreak()
        {
            return this.level == this.grade * 20;
        }
        public void LimitBreak()
        {
            if (CanLimitBreak() && grade < 4)
            {
                this.grade++;
            }
        }
    }
}
