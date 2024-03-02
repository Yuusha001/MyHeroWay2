using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "CharacterStatsBonus", menuName = "Character Stats/Character Stats Bonus")]
    public class CharacterStatsBonus : ScriptableObject
    {
        [ReadOnly]
        public List<StatsBonus> statsEntries = new List<StatsBonus>();

        [System.Serializable]
        public class StatsBonus
        {
            [ReadOnly]
            public int level;
            [ReadOnly]
            public Vector2Int hpBonus;
            [ReadOnly]
            public Vector2Int mpBonus;
        }
    }
}
