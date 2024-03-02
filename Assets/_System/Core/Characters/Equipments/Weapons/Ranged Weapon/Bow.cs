using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class Bow : RangeWeapon
    {
        public Vector2 posSpawn;
        public Vector2 sizeCollider;
        public Vector2 offsetCollider;

        public override void Initialize(Controller controller)
        {
            base.Initialize(controller);
            this.weaponType = EWeaponType.Bow;
        }

        public override void OnEquip()
        {
            base.OnEquip();
        }

        public override void OnUnEquip()
        {
            base.OnUnEquip();
        }

        protected override void OnEvent(string obj)
        {
            if (obj.Equals("EmptyState"))
            {
                isActiveCombo = false;
            }
            if (obj.Equals(StrManager.TriggerDamageEvent))
            {
                //AudioManager.Instance.PlayOneShot(shootSfx, 1f);
                var hitDirection = controller.core.movement.facingDirection;
                DamageInfo damageInfo = new DamageInfo(controller.core);
                posSpawn = handleFacingPosition(hitDirection);
                damageInfo.SetupCombo(weaponMoveSets.moveSets[0]);
                if (controller is PlayerController)
                {
                    var characterEquipment = PlayerControlManager.Instance.playerController.characterEquipment;
                    var primary = characterEquipment.primaryWeapon;
                    var secondary = characterEquipment.secondaryWeapon;
                    if (secondary != null)
                    {
                        damageInfo.SetupWeaponData(weaponType, isPrimaryWeapon, primary.weaponStats, secondary.weaponStats);
                    }
                    else
                    {
                        damageInfo.SetupWeaponData(weaponType, isPrimaryWeapon, primary.weaponStats);

                    }
                }
                else
                {
                    damageInfo.SetupWeaponData(weaponType, isPrimaryWeapon, this.weaponStats);
                }

                Projectile p = FactoryObject.Spawn<Projectile>(StrManager.ProjectilePool, StrManager.ArrowGreenProjectile);
                p.transform.position = posSpawn;
                
                numProjectile--;
                if (numProjectile <= 0)
                {
                    numProjectile = 0;
                    reloadTimer = coolDown;
                }

                p.Initialize(damageInfo);
                isActiveCombo = false;
            }
        }

        public override void TriggerWeapon()
        {
            if (numProjectile <= 0) return;
            if (!controller.IsInteracting)
            {
                isActiveCombo = true;
                var currentMoveSet = weaponMoveSets.moveSets[0];
                controller.IsInteracting = true;
                controller.animatorHandle.SetBool(StrManager.isInteracting, controller.IsInteracting);
                controller.animatorHandle.SetBool(currentMoveSet.animationName, true);
            }
        }
        public override void OnUpdate()
        {
            if (numProjectile > 0) return;
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                numProjectile = totalProjectile;
            }
        }

        public override void SetUpPassive(CharacterStats originalStats)
        {

        }

        public Vector3 handleFacingPosition(EFacingDirection facingDirection)
        {
            Vector3 pos = Vector3.zero;
            switch (controller.GetMovement().facingDirection)
            {
                case EFacingDirection.DOWN:
                    pos = controller.transform.position + new Vector3(offsetCollider.x, offsetCollider.y * -1);
                    break;
                case EFacingDirection.LEFT:
                    pos = controller.transform.position + new Vector3(offsetCollider.y * -1, offsetCollider.x);
                    break;
                case EFacingDirection.RIGHT:
                    pos = controller.transform.position + new Vector3(offsetCollider.y * 1, offsetCollider.x);
                    break;
                case EFacingDirection.UP:
                    pos = controller.transform.position + new Vector3(offsetCollider.x, offsetCollider.y * 1);
                    break;
            }
            return pos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (controller == null)
            {
                Vector3 pos = transform.position + (Vector3)offsetCollider;
                pos.z = 0;
                Gizmos.DrawWireCube(pos, sizeCollider);
            }
            else
            {
                Vector3 pos = handleFacingPosition(controller.GetMovement().facingDirection);
                Gizmos.DrawWireCube(pos, sizeCollider);
            }
        }
    }

}

