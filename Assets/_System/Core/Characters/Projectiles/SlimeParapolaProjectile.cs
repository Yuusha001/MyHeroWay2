using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class SlimeParapolaProjectile : ParapolaProjectile
    {
        public override void Initialize(DamageInfo damageInfo, Vector3 endPos)
        {
            base.Initialize(damageInfo, endPos);
            if (damageInfo.owner.controller is EnemyController)
            {
                if(damageInfo.owner.controller.TryGetComponent(out EnemyController enemy)){
                    var enemyDictionary = DataManager.Instance.enemyDictionary;
                    var color = enemyDictionary.GetEnemyData(enemy.Type).GetData(enemy.Name).color;
                    display.GetComponent<SpriteRenderer>().color = color;
                    impact.GetComponent<SpriteRenderer>().color = color;
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

        protected override void OnComplete()
        {
            if (!display.gameObject.activeSelf)
            {
                impact.gameObject.SetActive(true);
                impact.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            }
        }
    }
}
