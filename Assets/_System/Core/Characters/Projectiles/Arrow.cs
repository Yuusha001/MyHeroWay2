using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class Arrow : Projectile
    {
        protected override void OnCollision(Collider2D other)
        {
            if (!isActive) return;
            if ((layerContact & (1 << other.gameObject.layer)) == 0) return;
            if (other.GetInstanceID().Equals(damageInfo.idSender)) return;
            IDamage id = other.GetComponent<IDamage>();
            if (id != null && id.controller.GetDamageSenderType() == damageInfo.damageSenderType) return;
            impact.gameObject.SetActive(true);

            isActive = false;
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                GetComponent<Collider2D>().enabled = false;
                display.gameObject.SetActive(false);
            }
            else
            {
                GetComponent<Collider2D>().enabled = false;
                display.gameObject.SetActive(false);
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
