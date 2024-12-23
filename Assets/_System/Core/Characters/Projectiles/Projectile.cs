using DG.Tweening;
using PathologicalGames;
using MyHeroWay;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    public bool isAutoTarget = false;
    [SerializeField] private float timeTarget = 0.2f;
    private float currentTime;
    public Transform display;
    public Transform impact;
    public bool isActive;
    protected bool isPooled;
    protected DamageInfo damageInfo;
    protected Vector2 direction;
    public LayerMask layerContact;
    private float lifeTimer;
    public Vector3 hitTransform;
    protected event Action<List<IDamage>> OnContact;
    public virtual void Initialize(DamageInfo damageInfo)
    {
        this.damageInfo = damageInfo;
        GetComponent<Collider2D>().enabled = true;
        if (display != null)
        {
            display.gameObject.SetActive(true);
        }
        if (impact != null)
            impact.gameObject.SetActive(false);
        isActive = true;
        isPooled = false;
        lifeTimer = 0;
        currentTime = timeTarget;
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
            if (isAutoTarget)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    direction = damageInfo.owner.transform.position - transform.position;
                    direction.Normalize();
                }
                transform.position += (Vector3)direction * speed * Time.deltaTime;
            }
            else
            {
                transform.position += speed * Time.deltaTime * (Vector3)direction;
            }
        }
    }

    private void Update()
    {
        UpdateLogic();
        HandleLifeTime();
    }
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
                if(impact.TryGetComponent(out ParticleSystem vfx))
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
