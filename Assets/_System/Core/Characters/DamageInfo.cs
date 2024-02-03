using UnityEngine;

namespace MyHeroWay
{
    [System.Serializable]
    public class DamageInfo
    {
        public int idSender;
        public Controller owner;
        public int damage;
        public EDamageType damageType;
        public EFacingDirection hitDirection;
        public bool isKnockBack;
        public Vector2 force;
        public float stunTime;
        public EDamageSenderType damageSenderType;
        public System.Action<bool,string> onHitSuccess;
        public AudioClip impactSound;
        //public List<StatusEffectData> listEffect;
    }
}
