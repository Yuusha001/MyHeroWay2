using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "EquipmentData", menuName = "Equipment/EquipmentData", order = 0)]
    public class EquipmentDataSO : ScriptableObject
    {
        public int id;
        public GameObject prefab;
        [ShowAssetPreview]
        public Sprite icon;
        public EItemType itemType;
        public string itemName;
        [ResizableTextArea]
        public string description;
        public EEquipmentType equipmentType;
        [ResizableTextArea]
        public string[] gradeSkillDesc;
        [ResizableTextArea]
        public string[] gradeSkillDescDefault;
    }
}