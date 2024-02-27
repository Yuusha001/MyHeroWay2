using Cysharp.Threading.Tasks;
using MyHeroWay.Damage;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    [System.Serializable]
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class Combat : CoreComponent, IDamage, ICharacterStats
    {
        public bool isStunning;
        public bool isInvincible;
        private float stunTimer;
        private Collider2D selfCollider;
        public Transform effectDisplayHolder;
        public EDamageSenderType damageSenderType;
        public Controller controller => core.controller;
        public CharacterStats originalStats => core.controller.originalStats;
        public CharacterStats runtimeStats => core.controller.runtimeStats;
        public System.Action OnStatsChange;
        public System.Action OnGetHit;

        public int GetColliderInstanceID
        {
            get { return selfCollider.GetInstanceID(); }
        }

        public bool IsSelfCollider(Collider2D other)
        {
            return selfCollider.Equals(other);
        }

        public override void Initialize(Core core)
        {
            selfCollider = GetComponent<Collider2D>();
            base.Initialize(core);
        }

        public float NormalizeHealth()
        {
            if(runtimeStats.health == 0) return 0;
            return (float)runtimeStats.health / (float)originalStats.health;
        }
        public float NormalizeMana()
        {
            if (runtimeStats.mana == 0) return 0;
            return (float)runtimeStats.mana / (float)originalStats.mana;
        }

        public void ReduceHP(int amount)
        {
            if (amount <= 0) return;
            runtimeStats.health -= amount;
            if (runtimeStats.health <= 0)
            {
                runtimeStats.health = 0;
            }
            OnStatsChange?.Invoke();
        }

        public void GainHP(int amount)
        {
            if (amount <= 0) return;
            runtimeStats.health += amount;
            if (runtimeStats.health > originalStats.health)
            {
                runtimeStats.health = originalStats.health;
            }
            OnStatsChange?.Invoke();
        }

        public void ReduceMP(int amount)
        {
            if (amount <= 0) return;
            runtimeStats.mana -= amount;
            if (runtimeStats.mana <= 0)
            {
                runtimeStats.mana = 0;
            }
            OnStatsChange?.Invoke();
            
        }

        public void GainMP(int amount)
        {
            if (amount <= 0) return;
            runtimeStats.mana += amount;
            if (runtimeStats.mana > originalStats.mana)
            {
                runtimeStats.mana = originalStats.mana;
            }
            OnStatsChange?.Invoke();
            
        }

        [System.Obsolete]
        public async void TakeDamage(DamageInfo damageInfo, System.Action callBack = null)
        {
            if (damageInfo.damageSenderType == damageSenderType)
            {
                return;
            }
            if (core.controller.GetInstanceID() == damageInfo.idSender || isInvincible)
            {
                return;
            }
            
            int totalEva = await UniTask.Run(()=>DamageCaculation.TotalEva(damageInfo));
            bool isHit = damageInfo.sureHit ? true : DamageCaculation.IsHit(totalEva, damageInfo.owner.runtimeStats);
            bool isCrit = DamageCaculation.IsCrittical(damageInfo.owner.runtimeStats);
            bool isKnockBack = DamageCaculation.IsKnockBack(damageInfo, runtimeStats);

            float random = DamageCaculation.RandomByWeaponType(damageInfo.weaponType);
            var damageResult = await UniTask.Run(() => DamageCaculation.GetDamageResult(damageInfo, this.runtimeStats, isCrit, random));


            if (!isHit)
            {
                damageResult.isMiss = true;
                damageResult.damage = 0;
            }
            else
            {
                OnGetHit?.Invoke();

                if (isKnockBack)
                {
                    isStunning = isKnockBack;
                    stunTimer = damageInfo.stunTime;
                    Vector2 force = (this.transform.position - damageInfo.owner.transform.position).normalized * damageInfo.force;
                    core.movement.AddForce(force);
                
                }
            }
            ReduceHP((int)damageResult.damage);
            
            if (callBack != null)
            {
                callBack?.Invoke();
            }
            OnTakeDamage(damageResult);
            if (controller is EnemyController)
            {
               if(controller.TryGetComponent(out EnemyController enemyController)){
                    enemyController.target = damageInfo.owner.controller;
                    enemyController.SwitchState(enemyController.chasingState);
               }
            }
        }

        public void OnTakeDamage(DamageResult damageResult)
        {
            if(damageResult.isMiss)
            {
                var content = StrManager.Missed[Random.Range(0, StrManager.Missed.Length)];   
                var textDamage = DataManager.Instance.damageTextEff.Spawn(this.transform.position, content);
                textDamage.enableLeftText = true;
                textDamage.enableNumber = false;
                textDamage.SetColor(Color.HSVToRGB(Random.value, 0.5f, 1f));
                textDamage.SetFollowedTarget(this.transform);
            }
            else
            {
                var textDamage = DataManager.Instance.damageTextEff.Spawn(this.transform.position, (int)damageResult.damage);
                textDamage.enableLeftText = false;
                textDamage.enableNumber = true;
                switch (damageResult.damageType)
                {
                    case EDamageType.PHYSICAL:
                        textDamage.SetColor(Color.gray);
                        break;
                    case EDamageType.MAGICAL:
                        textDamage.SetColor(Color.magenta);
                        break;
                    case EDamageType.CRITICAL:
                        textDamage.SetColor(Color.yellow);
                        break;
                    case EDamageType.TRUEDAMAGE:
                        textDamage.SetColor(Color.white);
                        break;
                    default:
                        break;
                }
                textDamage.SetFollowedTarget(this.transform);
            }
            /* DamageNumberDisplay damageNumberDisplay = FactoryObject.Spawn<DamageNumberDisplay>(StrManager.VFXPool, StrManager.PixelTextEffect);
             damageNumberDisplay.Apply(effectDisplayHolder, (int)damageResult.damage);*/
           

        }


        public void UpdateLogic()
        {
            if (stunTimer > 0)
            {
               /* if (core.collisionSenses.IsTouchWallBehind())
                {
                    stunTimer -= Time.deltaTime * 3;
                }
                else*/
                {
                    stunTimer -= Time.deltaTime;
                }
            }
            else
            {
                isStunning = false;
                core.movement.SetVelocityZero();
            }
        }
#if UNITY_EDITOR
        private void Reset()
        {
            gameObject.layer = LayerMask.NameToLayer(StrManager.IDamageLayer);
            UnityEditor.EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}

