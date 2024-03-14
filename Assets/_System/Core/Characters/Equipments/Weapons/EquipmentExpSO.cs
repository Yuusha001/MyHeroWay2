using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "EquipmentExpSO", menuName = "Equipment/EquipmentExpSO")]
    public class EquipmentExpSO : ScriptableObject
    {
        [ReadOnly]
        public List<WeaponExp> expEntries = new List<WeaponExp>();

        [System.Serializable]
        public class WeaponExp
        {
            [ReadOnly]
            public int level;
            [ReadOnly]
            public int expToNext;
            [ReadOnly]
            public int totalExp;
        }

        public int GetEXP(int level)
        {
            for (int i = 0; i < expEntries.Count; i++)
            {
                if (expEntries[i].level == level)
                {
                    return expEntries[i].expToNext;
                }
            }
            return -1;
        }
    }
}
