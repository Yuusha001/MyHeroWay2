using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

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

        public int GetTotalEXP(int level)
        {
            for (int i = 0; i < expEntries.Count; i++)
            {
                if (expEntries[i].level == level)
                {
                    return expEntries[i].totalExp;
                }
            }
            return -1;
        }

        public int GetLimitBreakEXP(int grade)
        {
            for (int i = 0; i < expEntries.Count; i++)
            {
                if (expEntries[i].level == grade*20)
                {
                    return expEntries[i].totalExp;
                }
            }
            return -1;
        }
    }
}
