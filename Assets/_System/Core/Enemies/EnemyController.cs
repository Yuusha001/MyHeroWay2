using MyHeroWay.Stats;
using NaughtyAttributes;

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
            CaculateStats();
        }
        public virtual void UpdateLogic()
        {
            if (!IsActive) return;
            if (currentState != null)
            {
                currentState.UpdateLogic();
            }
            core.UpdateLogic();
            if (hpBar != null)
            {
                hpBar.UpdateBar(GetCombat().NormalizeHealth());
            }
            if (mpBar != null)
            {
                mpBar.UpdateBar(GetCombat().NormalizeMana());
            }
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

        public virtual void OnDead(bool deactiveCharacter)
        {
            IsActive = false;
            if (deactiveCharacter)
            {
                animatorHandle.DeactiveCharacter();
            }
        }

        [Button("Caculate Stats")]
        private void CaculateStats()
        {
            var data = DataManager.Instance.enemyDictionary.GetEnemyData(Type).GetData(Name);
            originalStats = StatsCaculation.GetFinalCharacterStats(currentLevel, data.characterStatsModifier);
            runtimeStats = originalStats;
        }
    }
}
