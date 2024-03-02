using UnityEngine;
using NaughtyAttributes;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "SpellStats", menuName = "Weapon/SpellStats", order = 1)]
    [System.Serializable]
    public class SpellStats : ScriptableObject
    {
        public int POW;
        public int ManaCost;
        public int CoolDown;
        public int KnockBackChance;
        [ResizableTextArea]
        public string Description;
        public EEffectStatus EffectStatus;
        public EElementType ElementType;
        public ESpellType SpellType;
    }
}
