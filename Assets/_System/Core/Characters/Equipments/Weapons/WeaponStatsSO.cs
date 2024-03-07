using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "WeaponStatsSO", menuName = "Weapon/WeaponStatsSO", order = 1)]
    [System.Serializable]
    public class WeaponStatsSO : ScriptableObject
    {
        public int ATK;
        public EWeaponType WeaponType;
        public EElementType ElementType;
        public EEffectStatus EffectStatus;
        public int Evasion;
        [BoxGroup("Sword, Axe, Hammer, Spear, Pole, Staves = 10")] 
        public int KnockBackChance;
    }
}
