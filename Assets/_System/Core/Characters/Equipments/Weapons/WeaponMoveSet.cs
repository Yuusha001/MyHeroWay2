using UnityEngine;

namespace MyHeroWay
{
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
