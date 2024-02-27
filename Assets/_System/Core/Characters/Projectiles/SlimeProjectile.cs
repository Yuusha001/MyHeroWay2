using System.Collections.Generic;
using UnityEngine;
using Utils.String;
using DG.Tweening;

namespace MyHeroWay
{
    public class SlimeProjectile : Projectile
    {

        public override void Initialize(DamageInfo damageInfo)
        {
            base.Initialize(damageInfo);
            if (damageInfo.owner.controller is EnemyController)
            {
                if (damageInfo.owner.controller.TryGetComponent(out EnemyController enemy))
                {
                    var enemyDictionary = DataManager.Instance.enemyDictionary;
                    var color = enemyDictionary.GetEnemyData(enemy.Type).GetData(enemy.Name).color;
                    display.GetComponent<SpriteRenderer>().color = color;
                    impact.GetComponent<SpriteRenderer>().color = color;
                    if(enemy.target == null)
                    {
                        Deactive(0);
                        return;
                    }
                    direction = enemy.target.transform.position - enemy.transform.position;

                }
            }
        }
        protected override void OnCollision(Collider2D other)
        {
            if (!isActive) return;
            if ((layerContact & (1 << other.gameObject.layer)) == 0) return;
            if (other.GetInstanceID().Equals(damageInfo.idSender)) return;
            IDamage id = other.GetComponent<IDamage>();
            if (id != null && id.controller.GetDamageSenderType() == damageInfo.damageSenderType) return;
            GetComponent<Collider2D>().enabled = false;
            display.gameObject.SetActive(false);
            impact.gameObject.SetActive(true);
            impact.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            isActive = false;
            if (other.gameObject.layer == LayerMask.NameToLayer(StrManager.ObstacleLayer))
            {
                Debug.Log("Hit obs");
            }
            else
            {
                Debug.Log("Hit idmage");
                if (other.TryGetComponent(out IDamage damages))
                {
                    List<IDamage> idamages = new List<IDamage>
                    {
                        damages
                    };
                    OnContactCollision(idamages);
                }
            }
        }
    }
}
