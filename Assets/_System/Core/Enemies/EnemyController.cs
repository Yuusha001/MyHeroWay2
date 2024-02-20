using MyHeroWay.Stats;
using NaughtyAttributes;
using System;

namespace MyHeroWay
{
    public abstract class EnemyController : Controller
    {
        public EEnemyName Name;
        public EEnemyType Type;
        public int currentLevel;
        [Foldout("Animator")]
        public EnemyAnimator enemyAnimator;
        public AIState currentState;
        public SpriteBar hpBar;
        public SpriteBar mpBar;
        public virtual void Initialize()
        {
            core.Initialize(this);
            enemyAnimator.Initialize(this);
            isActive = true;
            CaculateStats();
            GetCombat().OnStatsChange += StatsChangeHandler;
        }

        private void StatsChangeHandler()
        {
            if (hpBar != null)
                hpBar.UpdateBar(GetCombat().NormalizeHealth());
            if (mpBar !=null)
                mpBar.UpdateBar(GetCombat().NormalizeMana());
            if (runtimeStats.health == 0 && isActive)
            {
                Die(true);
            }
        }

        public virtual void UpdateLogic()
        {
            if (!IsActive) return;
            if (currentState != null)
            {
                currentState.UpdateLogic();
            }
            core.UpdateLogic();
        }
        public virtual void UpdatePhysic()
        {
            if (!IsActive) return;
            if (currentState != null)
            {
                currentState.UpdatePhysic();
            }
            core.UpdatePhysic();
        }

        public override void Die(bool deactiveCharacter)
        {
            base.Die(deactiveCharacter);
            if (hpBar != null)
                hpBar.Deactive();
            if (mpBar != null)
                mpBar.Deactive();
            GetCombat().OnStatsChange -= StatsChangeHandler;
        }

        [Button("Caculate Stats")]
        private void CaculateStats()
        {
            var data = DataManager.Instance.enemyDictionary.GetEnemyData(Type).GetData(Name);
            originalStats = StatsCaculation.GetFinalCharacterStats(currentLevel, data.characterStatsModifier);
            runtimeStats = new CharacterStats() + StatsCaculation.GetFinalCharacterStats(currentLevel, data.characterStatsModifier);
        }
    }
}
