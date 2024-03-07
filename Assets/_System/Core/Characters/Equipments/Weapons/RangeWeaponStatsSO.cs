using UnityEngine;

namespace MyHeroWay.SciptableObject
{
    [CreateAssetMenu(fileName = "RangeWeaponStatsSO", menuName = "Weapon/RangeWeaponStatsSO", order = 2)]
    public class RangeWeaponStatsSO : WeaponStatsSO
    {
        public int totalProjectile;
        public float coolDown;

    }
}
