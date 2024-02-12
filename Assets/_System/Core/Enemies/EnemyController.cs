using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public abstract class EnemyController : Controller
    {
        public EnemyAnimator enemyAnimator;
        public AIState currentState;

        public virtual void Initialize()
        {
            core.Initialize(this);
            enemyAnimator.Initialize(this);
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

        public virtual void OnDead(bool deactiveCharacter)
        {
            IsActive = false;
            if (deactiveCharacter)
            {
                animatorHandle.DeactiveCharacter();
            }
        }
    }
}
