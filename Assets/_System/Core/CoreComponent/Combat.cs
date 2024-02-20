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
        public EDamageSenderType damageSenderType;
        public Controller controller => core.controller;
        public CharacterStats originalStats => core.controller.originalStats;
        public CharacterStats runtimeStats => core.controller.runtimeStats;
        public System.Action OnStatsChange;

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
            Debug.Log("reduce " +  amount);
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
            string content = "<color=green>+" + amount + "</color>";
            //UltimateTextDamageManager.Instance.Add(content, effectDisplayHolder.position);
           

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
            string content = "<color=blue>-" + amount + "</color>";
            //UltimateTextDamageManager.Instance.Add(content, effectDisplayHolder.position);
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
            string content = "<color=blue>+" + amount + "</color>";
            //UltimateTextDamageManager.Instance.Add(content, effectDisplayHolder.position);
        }

        public void TakeDamage(DamageInfo damageInfo, System.Action callBack = null)
        {
            if (damageInfo.damageSenderType == damageSenderType)
            {
                return;
            }
            if (core.controller.GetInstanceID() == damageInfo.idSender || isInvincible)
            {
                return;
            }
            controller.animatorHandle.SetBool(StrManager.getHit,true);
            var damageResult = DamageCaculation.GetDamageResult(damageInfo, this.runtimeStats);
            ReduceHP((int)damageResult.damage);
            isStunning = damageResult.canKnockBack;
            stunTimer = damageInfo.stunTime;
            if (damageResult.canKnockBack)
            {
                core.movement.SetVelocity(damageInfo.force);
            }
            if (callBack != null)
            {
                callBack?.Invoke();
            }
            OnTakeDamage(damageInfo);
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            /*string content = "";
            switch (damageInfo.damageType)
            {
                case EDamageType.PHYSICAL:
                    content = "<color=orange>-" + damageInfo.damage.ToString() + "</color>";
                    break;
                case EDamageType.MAGICAL:
                    break;
                case EDamageType.CRITICAL:
                    content = "<color=#FF2600><size=1100>-" + damageInfo.damage.ToString() + "</size></color>";
                    break;
                case EDamageType.TRUEDAMAGE:
                    content = "<color=white>-" + damageInfo.damage.ToString() + "</color>";
                    break;
                default:
                    break;
            }*/
            //UltimateTextDamageManager.Instance.Add(content, effectDisplayHolder.position);
        }


        public void UpdateLogic()
        {
            if (stunTimer > 0)
            {
                /*if (core.collisionSenses.IsTouchWallBehind())
                {
                    stunTimer -= Time.deltaTime * 3;
                }
                else
                {
                    stunTimer -= Time.deltaTime;
                }*/
            }
            else
            {
                isStunning = false;
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

