using UnityEngine;

namespace MyHeroWay
{
    public class Dagger : Weapon
    {
        public bool isTrigger;
        public bool isActiveCombo;
        public bool canDoCombo;
        public LayerMask layerContact;
        public Vector2 sizeCollider;
        public Vector2 offsetCollider;
        public override void SetEquipmentData(EquipmentData data)
        {
            base.SetEquipmentData(data);
            equipmentStats.physicalDamage = 0;
        }
        public override void SetUpPassive(CharacterStats originalStats)
        {

        }
        protected override void OnEvent(string obj)
        {
            if (!isTrigger) return;
            if (obj.Equals("PlayFX"))
            {

            }
            if (obj.Equals("TriggerDamage"))
            {
                var colls = new Collider2D[5];
                var hitDirection = controller.core.movement.facingDirection;
                Vector3 pos = handleFacingPosition(hitDirection);
                Physics2D.OverlapBoxNonAlloc(pos, sizeCollider, 0, colls, layerContact);
                DamageInfo damageInfo = new DamageInfo();
                //damageInfo.listEffect = new List<StatusEffectData>();
                damageInfo.damageSenderType = controller.core.combat.damageSenderType;
                damageInfo.hitDirection = hitDirection;

                bool isCrit = false;
                int damage = (int)controller.runtimeStats.physicalDamage;
               /* damageInfo.stunTime = moveSets[currentIndexMoveSet].stunTime;
                damageInfo.impactSound = moveSets[currentIndexMoveSet].impactSound;*/
                damageInfo.idSender = controller.GetInstanceID();
                damageInfo.owner = controller;

                damageInfo.damageType = isCrit ? EDamageType.CRITICAL : EDamageType.PHYSICAL;
                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i] == null) continue;
                    IDamage target = colls[i].GetComponent<IDamage>();
                    if (target != null)
                    {
                        damageInfo.damage = damage;
                        target.TakeDamage(damageInfo);
                    }
                }
                canDoCombo = false;
            }
            if (obj.Equals("ActiveCombo"))
            {
                canDoCombo = true;
                // isActiveCombo = true;
            }
            if (obj.Equals("DeactiveCombo"))
            {
                canDoCombo = false;
                isActiveCombo = false;
            }
            if (obj.Equals("EmptyState"))
            {
                isActiveCombo = false;
            }


        }
        public override void OnEquip()
        {
            base.OnEquip();
        }

        public override void OnUnEquip()
        {
            base.OnUnEquip();

        }
        public override void OnUpdate()
        {
            /*if (controller.isStunning)
            {
                isActiveCombo = false;
            }*/
        }
        private bool PredictHit()
        {
            var colls = new Collider2D[5];
            /*int hitDirection = controller.core.movement.facingDirection;
            Vector3 pos = controller.position + new Vector3(offsetCollider.x * hitDirection, offsetCollider.y);
            Physics2D.OverlapBoxNonAlloc(pos, sizeCollider, 0, colls, layerContact);
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i] == null) continue;
                IDamage target = colls[i].GetComponent<IDamage>();
                if (target != null && target.controller != controller)
                {
                    return true;
                }
            }*/
            return false;
        }
        public override void TriggerWeapon()
        {
            /* if (canDoCombo)
             {
                 currentIndexMoveSet++;
                 if (currentIndexMoveSet >= moveSets.Length)
                 {

                     currentIndexMoveSet = 0;
                 }
                 isActiveCombo = PredictHit();
                 var currentMoveSet = moveSets[currentIndexMoveSet];
                 controller.animatorHandle.PlayAnimation(currentMoveSet.animationName, 0.1f, 1, true, true);
                 canDoCombo = false;
             }
             else
             {
                 if (!controller.isInteracting && !canDoCombo)
                 {
                     isActiveCombo = PredictHit();
                     currentIndexMoveSet = 0;
                     var currentMoveSet = moveSets[currentIndexMoveSet];
                     controller.animatorHandle.PlayAnimation(currentMoveSet.animationName, 0.1f, 1, true, true);
                 }
             }*/
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

