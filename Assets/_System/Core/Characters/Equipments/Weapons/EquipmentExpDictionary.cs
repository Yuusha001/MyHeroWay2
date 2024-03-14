using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "EquipmentExpDictionary", menuName = "Equipment/EquipmentExpDictionary")]
    public class EquipmentExpDictionary : ScriptableObject
    {
        public EquipmentExpDictionaryItem[] data;

        public EquipmentExpSO GetWeaponExp(EEquipmentRarity rarity)
        {
            foreach (var item in data)
            {
                if (item.equipmentRarity == rarity)
                {
                    return item.data;
                }
            }
            return null;
        }
    }

    [System.Serializable]
    public class EquipmentExpDictionaryItem
    {
        public EEquipmentRarity equipmentRarity;
        public EquipmentExpSO data;
    }
}
