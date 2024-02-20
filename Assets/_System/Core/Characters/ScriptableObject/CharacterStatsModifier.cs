using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "CharacterStatsModifier", menuName = "Character/CharacterStatsModifier", order = 2)]
    [System.Serializable]
    public class CharacterStatsModifier : ScriptableObject
    {

        [BoxGroup("HP")]
        [Range(0, 60f)]
        public int baseHP;
        [BoxGroup("HP")]
        [Range(1, 1.7f)]
        public float modHP;

        [BoxGroup("MP")]
        [Range(0, 60f)]
        public int baseMP;
        [BoxGroup("MP")]
        [Range(0.4f, 0.7f)]
        public float modMP;

        [BoxGroup("STR")]
        [Range(0, 30f)]
        public int baseSTR;
        [BoxGroup("STR")]
        [Range(60, 70f)]
        public float modSTR;

        [BoxGroup("MAG")]
        [Range(0, 20)]
        public int baseMAG;
        [BoxGroup("MAG")]
        [Range(60, 70)]
        public float modMAG;

        [BoxGroup("VIT")]
        [Range(0, 25)]
        public int baseVIT;
        [BoxGroup("VIT")]
        [Range(30, 50f)]
        public float modVIT;
       
        [BoxGroup("MDEF")]
        [Range(0, 25)]
        public int baseMDEF;
        [BoxGroup("MDEF")]
        [Range(30, 50f)]
        public float modMDEF;

        [BoxGroup("SPD")]
        [Range(10, 99)]
        public int baseSPD;
        [BoxGroup("SPD")]
        [Range(15, 20)]
        public float modSPD;
    }
}
