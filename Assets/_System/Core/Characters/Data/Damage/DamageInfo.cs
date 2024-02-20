using UnityEngine;
using static MyHeroWay.WeaponMoveSets;

namespace MyHeroWay
{
    [System.Serializable]
    public class DamageInfo
    {
        public int idSender { private set; get; }
        public Combat owner { private set; get; }
        public bool fromPrimary { private set; get; }
        public bool canKnockBack { private set; get; }
        public float stunTime { private set; get; }
        public Vector2 force { private set; get; }
        public EFacingDirection hitDirection { private set; get; }
        public EDamageSenderType damageSenderType { private set; get; }
        public EWeaponType weaponType { private set; get; }
        public WeaponStats primaryWeaponStats { private set; get; }
        public WeaponStats secondaryWeaponStats { private set; get; }
        public SpellStats spellStats { private set; get; }
        public AudioClip impactSound { private set; get; }

        public DamageInfo(Core owner)
        {
            this.idSender = owner.controller.GetInstanceID();
            this.owner = owner.combat;
            this.damageSenderType = owner.combat.damageSenderType;
            this.hitDirection = owner.movement.facingDirection;
        }

        public void SetupCombo(WeaponMoveSet moveSet)
        {
            this.stunTime = moveSet.stunTime;
            this.impactSound = moveSet.impactSound;
            this.canKnockBack = moveSet.force != Vector2.zero;
            this.force = moveSet.force;
        }

        public void SetupWeaponData(EWeaponType weaponType, bool isPrimary ,WeaponStats primary, WeaponStats secondary = null)
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
