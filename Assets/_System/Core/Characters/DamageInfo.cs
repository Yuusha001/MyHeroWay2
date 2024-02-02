using UnityEngine;

namespace MyHeroWay
{
    [System.Serializable]
    public class DamageInfo
    {
        public int idSender;
        public Controller owner;
        public int damage;
        public DamageType damageType;
        public FacingDirection hitDirection;
        public bool isKnockBack;
        public Vector2 force;
        public float stunTime;
        public DamageSenderType damageSenderType;
        public System.Action<bool,string> onHitSuccess;
        public AudioClip impactSound;
        //public List<StatusEffectData> listEffect;
    }
}
