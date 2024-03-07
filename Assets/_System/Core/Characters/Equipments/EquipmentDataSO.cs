using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "EquipmentData", menuName = "Item/EquipmentData", order = 1)]
    public class EquipmentDataSO : ItemDataSO
    {
        public EEquipmentType equipmentType;
        [ResizableTextArea]
        public string[] gradeSkillDesc;
        [ResizableTextArea]
        public string[] gradeSkillDescDefault;
    }
}