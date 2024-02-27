using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class BasicEnemyRangeWeapon : Weapon
    {
        public string projectileName;
        public int numProjectile;
        public float attackPerSecond;
        private const int MaxProjectile = 15;
        private const float ReloadTime = 3;
        private float reloadTimer;
        private float attackTimer;
        public Vector2 posSpawn;
        public bool parapolaProjectile;

        public override void Initialize(Controller controller)
        {
            base.Initialize(controller);
            this.weaponType = EWeaponType.Pole;
            attackTimer = attackPerSecond;
        }
        public override void TriggerWeapon()
        {
            if (numProjectile <= 0) return;
            attackTimer -= Time.deltaTime;
            if (attackTimer > 0) return;
            if (!controller.IsInteracting)
            {
                isActiveCombo = true;
                var currentMoveSet = weaponMoveSets.moveSets[0];
                controller.IsInteracting = true;
                controller.animatorHandle.SetBool(StrManager.isInteracting, controller.IsInteracting);
                controller.animatorHandle.SetBool(currentMoveSet.animationName, true);
                attackTimer = attackPerSecond;
            }
        }

        public override void OnUpdate()
        {

            if (numProjectile > 0) return;
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                numProjectile = MaxProjectile;
            }
        }

        protected override void OnEvent(string obj)
        {
            if (obj.Equals(StrManager.TriggerDamageEvent))
            {
                //AudioManager.Instance.PlayOneShot(shootSfx, 1f);
                var hitDirection = controller.core.movement.facingDirection;
                DamageInfo damageInfo = new DamageInfo(controller.core);
                posSpawn = damageInfo.owner.transform.position;
                damageInfo.SetupCombo(weaponMoveSets.moveSets[0]);
                damageInfo.SetupWeaponData(weaponType, isPrimaryWeapon, this.weaponStats);

                if (!parapolaProjectile)
                {
                    Projectile p = FactoryObject.Spawn<Projectile>(StrManager.ProjectilePool, projectileName);
                    p.transform.position = posSpawn;
                    p.Initialize(damageInfo);
                }
                else
                {
                    if (controller.TryGetComponent(out EnemyController enemy) && enemy.target != null)
                    {
                        ParapolaProjectile p = FactoryObject.Spawn<ParapolaProjectile>(StrManager.ProjectilePool, projectileName);
                        p.Initialize(damageInfo, enemy.target.transform.position);
                    }
                    else
                    {
                        isActiveCombo = false;
                        return;
                    }
                    
                }

                numProjectile--;
                if (numProjectile <= 0)
                {
                    numProjectile = 0;
                    reloadTimer = ReloadTime;
                }
                isActiveCombo = false;
            }
        }
    }
}
