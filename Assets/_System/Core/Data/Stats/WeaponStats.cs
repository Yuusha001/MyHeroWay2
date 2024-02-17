using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "Weapon/WeaponStats", order = 1)]
    [System.Serializable]
    public class WeaponStats : ScriptableObject
    {
        public int ATK;
        public EWeaponType WeaponType;
        public EElementType ElementType;
        public EEffectStatus EffectStatus;
        public int Evasion;
        [BoxGroup("Sword, Axe, Hammer, Spear, Pole, Staff = 10")] 
        public int KnockBackChance;
    }
}
