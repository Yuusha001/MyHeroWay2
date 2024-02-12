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
    private event Action<List<IDamage>> OnContact;
    public virtual void Initialize(DamageInfo damageInfo, Action<List<IDamage>> OnContact = null)
    {
        this.damageInfo = damageInfo;
        GetComponent<Collider2D>().enabled = true;
        if (display != null)
        {
            display.gameObject.SetActive(true);
            direction = transform.position - damageInfo.owner.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            display.eulerAngles = new Vector3(0, 0, angle-90);
        }
        if (impact != null)
            impact.gameObject.SetActive(false);
        isActive = true;
        isPooled = false;
        lifeTimer = 0;
        currentTime = timeTarget;

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
        OnCollision(other);
        hitTransform = other.transform.position;
    }
    protected void Deactive(float time)
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
                if(impact.TryGetComponent<Animation>(out Animation animation))
                {
                    animation.Play(StrManager.ArrowImpactVFX);
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
