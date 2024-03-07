using UnityEngine;
using static MyHeroWay.WeaponMoveSets;

namespace MyHeroWay
{
    [System.Serializable]
    public class DamageInfo
    {
        public int idSender { private set; get; }
        public Combat owner { private set; get; }
        public bool sureHit { private set; get; }
        public bool fromPrimary { private set; get; }
        public bool canKnockBack { private set; get; }
        public float stunTime { private set; get; }
        public float force { private set; get; }
        public EFacingDirection hitDirection { private set; get; }
        public EDamageSenderType damageSenderType { private set; get; }
        public EWeaponType weaponType { private set; get; }
        public WeaponStatsSO primaryWeaponStats { private set; get; }
        public WeaponStatsSO secondaryWeaponStats { private set; get; }
        public SpellStats spellStats { private set; get; }
        public AudioClip impactSound { private set; get; }

        public DamageInfo(Core owner, CollisionDamage collisionDamage = null)
        {
            this.idSender = owner.controller.GetInstanceID();
            this.owner = owner.combat;
            this.damageSenderType = owner.combat.damageSenderType;
            this.hitDirection = owner.movement.facingDirection;
            this.sureHit = collisionDamage != null ? true : false;
        }

        public void SetupCombo(WeaponMoveSet moveSet)
        {
            this.stunTime = moveSet.stunTime;
            this.impactSound = moveSet.impactSound;
            this.canKnockBack = moveSet.force != 0;
            this.force = moveSet.force;
        }

        public void SetupWeaponData(EWeaponType weaponType, bool isPrimary ,WeaponStatsSO primary, WeaponStatsSO secondary = null)
        {
            this.fromPrimary = isPrimary;
            this.weaponType = weaponType;
            this.primaryWeaponStats = primary;
            this.secondaryWeaponStats = secondary;
        }

        public void SetupSpellData(SpellStats spellStats)
        {
            this.spellStats = spellStats;
        }

        public void SetupSound(AudioClip audio)
        {
            this.impactSound = audio;
        }
    }
}
