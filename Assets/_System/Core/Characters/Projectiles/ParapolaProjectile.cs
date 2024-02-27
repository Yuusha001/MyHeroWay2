using PathologicalGames;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public abstract class ParapolaProjectile : MonoBehaviour
    {
        public AnimationCurve animationCurve;
        public float duration;
        public float heightY;
        private float currentTime;
        public Transform display;
        public Transform shadow;
        public Transform impact;
        public bool isActive;
        protected bool isPooled;
        protected DamageInfo damageInfo;
        protected Vector2 direction;
        public LayerMask layerContact;
        private float lifeTimer;
        public Vector3 hitTransform;
        public Vector3 startPosition;
        public Vector3 endPosition;
        protected event Action<List<IDamage>> OnContact;
        public virtual void Initialize(DamageInfo damageInfo, Vector3 endPos)
        {
            this.damageInfo = damageInfo;
            GetComponent<Collider2D>().enabled = true;
            if (display != null)
            {
                display.gameObject.SetActive(true);
            }
            if (impact != null)
            {
                impact.gameObject.SetActive(false);
            }
            startPosition = damageInfo.owner.transform.position;
            endPosition = endPos;
            isActive = true;
            isPooled = false;
            lifeTimer = duration + 0.5f;
            currentTime = 0;
            OnContact += (idamages) =>
            {
                for (int i = 0; i < idamages.Count; i++)
                {
                    idamages[i].TakeDamage(damageInfo);
                }
            };
        }
        public virtual void UpdateLogic()
        {
            if (isActive)
            {
                if (currentTime <= duration)
                {
                    currentTime += Time.deltaTime;
                    float linearT = currentTime / duration;
                    float heightT = animationCurve.Evaluate(linearT);
                    float height = Mathf.Lerp(0f, heightY, heightT);
                    transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);
                    shadow.transform.position = new Vector2(transform.position.x, transform.position.y-height-0.3f);
                }
                else 
                {
                    OnComplete();
                }
            }
        }

        private void Update()
        {
            UpdateLogic();
            HandleLifeTime();
        }

        protected abstract void OnComplete();

        protected abstract void OnCollision(Collider2D other);
        private void OnTriggerEnter2D(Collider2D other)
        {
            hitTransform = other.transform.position;
            OnCollision(other);
        }
        protected void Deactive(float time = 0)
        {
            lifeTimer = time;
            if (time <= 0)
            {
                isPooled = true;
                FactoryObject.Despawn(StrManager.ProjectilePool, this.transform, PoolManager.Pools[StrManager.ProjectilePool].transform);
            }
        }
        void HandleLifeTime()
        {
            if (lifeTimer > 0 && !isPooled)
            {
                lifeTimer -= Time.deltaTime;
                if (lifeTimer <= 0)
                {
                    lifeTimer = 0;
                    isPooled = true;
                    FactoryObject.Despawn(StrManager.ProjectilePool, this.transform, PoolManager.Pools[StrManager.ProjectilePool].transform);
                }
                else if (lifeTimer <= 0.1f)
                {
                    impact.gameObject.SetActive(true);
                    if (impact.TryGetComponent(out ParticleSystem vfx))
                    {
                        vfx.Play();
                    }
                }
            }
        }
        protected void OnContactCollision(List<IDamage> listContact)
        {
            OnContact?.Invoke(listContact);
            OnContact = null;
        }
    }
}
