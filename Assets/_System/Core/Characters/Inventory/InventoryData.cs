using System.Collections.Generic;

namespace MyHeroWay
{
    [System.Serializable]
    public class InventoryData
    {
        public InventoryData()
        {
            equipmentsOwned = new List<EquipmentData>();
            materialOwned = new List<MaterialData>();
            idItemDefine = 1;
        }
        public int idItemDefine;
        public List<EquipmentData> equipmentsOwned;
        public List<MaterialData> materialOwned;
    }
}
