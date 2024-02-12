using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "CharacterStatsModifier", menuName = "Character/CharacterStatsModifier", order = 2)]
    [System.Serializable]
    public class CharacterStatsModifier : ScriptableObject
    {
        [BoxGroup("HP")]
        public int baseHP;
        [BoxGroup("HP")]
        public float modHP;
        [BoxGroup("MP")]
        public int baseMP;
        [BoxGroup("MP")]
        public float modMP;
        [BoxGroup("STR")]
        public int baseSTR;
        [BoxGroup("STR")]
        public float modSTR;
        [BoxGroup("MAG")]
        public int baseMAG;
        [BoxGroup("MAG")]
        public float modMAG;
        [BoxGroup("VIT")]
        public int baseVIT;
        [BoxGroup("VIT")]
        public float modVIT;
        [BoxGroup("SPD")]
        public int baseSPD;
        [BoxGroup("SPD")]
        public float modSPD;
        [BoxGroup("MDEF")]
        public int baseMDEF;
        [BoxGroup("MDEF")]
        public float modMDEF;
    }
}
