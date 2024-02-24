using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class SmileController : EnemyController
    {
        public CollisionDamage collisionDamage;
        public override void Initialize()
        {
            base.Initialize();
            attackingState = new AIBasicAttacking(this, StrManager.AttackingState);
            chasingState = new AIBasicChasing(this, StrManager.ChasingState);
            wanderingState = new AIBasicWandering(this, StrManager.WanderingState);
            collisionDamage.Initialize(this);
        }

        private void Start()
        {
            Initialize();
            SwitchState(wanderingState);
        }

        private void Update()
        {
            UpdateLogic();
            collisionDamage.UpdateLogic();
        }

        private void FixedUpdate()
        {
            UpdatePhysic();
        }
    }
}
