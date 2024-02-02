using UnityEngine;
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
        public DamageSenderType damageSenderType;
        public Controller controller => core.controller;
        public CharacterStats originalStats => core.controller.originalStats;
        public CharacterStats runtimeStats => core.controller.runtimeStats;

        public event System.Action OnCrit;
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
            return (float)runtimeStats.health / (float)originalStats.health;
        }
        public float NormalizeMana()
        {
            return (float)runtimeStats.mana / (float)originalStats.mana;
        }

        public bool IsCrit()
        {
            bool isCrit = Random.Range(0, 100) < runtimeStats.critRate;
            if (isCrit)
            {
                OnCrit?.Invoke();
            }
            return isCrit;
        }

        public void ReduceHP(int amount)
        {
            if (amount <= 0) return;
            runtimeStats.health -= amount;
            if (runtimeStats.health <= 0)
            {
                runtimeStats.health = 0;
            }
        }

        public void GainHP(int amount)
        {
            if (amount <= 0) return;
            runtimeStats.health += amount;
            if (runtimeStats.health > originalStats.health)
            {
                runtimeStats.health = originalStats.health;
            }
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
            string content = "<color=blue>+" + amount + "</color>";
            //UltimateTextDamageManager.Instance.Add(content, effectDisplayHolder.position);
        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            if (damageInfo.damageSenderType == damageSenderType)
            {
                damageInfo.onHitSuccess?.Invoke(false,"");
                return;
            }
            if (core.controller.GetInstanceID() == damageInfo.idSender || isInvincible)
            {
                damageInfo.onHitSuccess?.Invoke(false, "Miss");
                return;
            }
            
            isStunning = damageInfo.isKnockBack;
            stunTimer = damageInfo.stunTime;
            if (damageInfo.isKnockBack)
            {
                core.movement.SetVelocity(damageInfo.force);
            }
            damageInfo.onHitSuccess?.Invoke(true,"");
            OnTakeDamage(damageInfo);
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            string content = "";
            switch (damageInfo.damageType)
            {
                case DamageType.PHYSICAL:
                    content = "<color=orange>-" + damageInfo.damage.ToString() + "</color>";
                    break;
                case DamageType.MAGICAL:
                    break;
                case DamageType.CRITICAL:
                    content = "<color=#FF2600><size=1100>-" + damageInfo.damage.ToString() + "</size></color>";
                    break;
                case DamageType.TRUEDAMAGE:
                    content = "<color=white>-" + damageInfo.damage.ToString() + "</color>";
                    break;
                default:
                    break;
            }
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
            gameObject.layer = LayerMask.NameToLayer("IDamage");
            UnityEditor.EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}

