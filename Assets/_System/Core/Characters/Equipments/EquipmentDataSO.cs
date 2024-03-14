using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "EquipmentData", menuName = "Item/EquipmentData", order = 1)]
    public class EquipmentDataSO : ItemDataSO
    {
        public EEquipmentRarity equipmentRarity;
        public EEquipmentType equipmentType;
        public EElementType equipmentElementType;
        [TextArea(0, 3), ResizableTextArea]
        public string[] gradeSkillDesc;
        [ShowAssetPreview]
        public Sprite guiIcon;
    }
}