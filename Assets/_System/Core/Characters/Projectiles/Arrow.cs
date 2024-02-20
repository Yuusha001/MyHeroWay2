using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class Arrow : Projectile
    {
        public override void Initialize(DamageInfo damageInfo)
        {
            base.Initialize(damageInfo);
            GetComponent<TrailRenderer>().enabled = true;
        }

        protected override void OnCollision(Collider2D other)
        {
            if (!isActive) return;
            if ((layerContact & (1 << other.gameObject.layer)) == 0) return;
            if (other.GetInstanceID().Equals(damageInfo.idSender)) return;
            IDamage id = other.GetComponent<IDamage>();
            if (id != null && id.controller.GetDamageSenderType() == damageInfo.damageSenderType) return;
            display.gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
            impact.gameObject.SetActive(true);
            impact.GetComponent<ParticleSystem>().Play();
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
