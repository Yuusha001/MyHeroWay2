using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class CollisionDamage : MonoBehaviour
    {
        EnemyController enemyController;

        public LayerMask detectionMask;
        public float sensorRange;
        public HashSet<Collider2D> damagedTargets; // Use HashSet to store damaged targets
        public void Initialize(EnemyController _enemyController)
        {
            enemyController = _enemyController;
            damagedTargets = new HashSet<Collider2D>(); // Initialize the HashSet
        }

        public void UpdateLogic()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position,
               sensorRange,
               detectionMask).ToList();

            foreach (var collider in colliders)
            {
                if (!collider.transform.CompareTag(StrManager.EnemyTag) && !damagedTargets.Contains(collider))
                {
                    if (collider.TryGetComponent(out IDamage damage))
                    {
                        enemyController.enemyAnimator.SetBool(StrManager.attackAnimation, true);
                        DamageInfo damageInfo = new DamageInfo(enemyController.core, this);
                        var weapon = enemyController.weapon;
                        damageInfo.SetupWeaponData(weapon.weaponType, true, weapon.weaponStats);
                        damageInfo.SetupCombo(weapon.weaponMoveSets.moveSets[0]);
                        damage.TakeDamage(damageInfo,() => Invoke(nameof(Clear),1f));
                        damagedTargets.Add(collider); // Add collider to the HashSet
                    }
                }
            }
        }

        private void Clear()
        {
            this.damagedTargets.Clear();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, sensorRange);
        }
    }
}
