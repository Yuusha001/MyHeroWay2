using UnityEngine;

namespace MyHeroWay.SciptableObject
{
    [CreateAssetMenu(fileName = "RangeWeaponStats", menuName = "Weapon/RangeWeaponStats", order = 2)]
    public class RangeWeaponStats : WeaponStats
    {
        public int totalProjectile;
        public float coolDown;

    }
}
