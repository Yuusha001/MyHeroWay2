using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "WeaponMoveSets", menuName = "Weapon/WeaponMoveSet", order = 1)]
    [System.Serializable]
    public class WeaponMoveSets : ScriptableObject
    {
        public WeaponMoveSet[] moveSets;

        [System.Serializable]
        public class WeaponMoveSet
        {
            public string animationName;
            public int layer;
            public float stunTime;
            public Vector2 force;
            public AudioClip activeSound;
            public AudioClip impactSound;
        }
    }
}
