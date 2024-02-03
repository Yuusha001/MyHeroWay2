using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "WeaponMoveSets", menuName = "Weapon/WeaponMoveSet", order = 1)]
    public class WeaponMoveSets : ScriptableObject
    {
        public WeaponMoveSet[] moveSets;
    }
}
