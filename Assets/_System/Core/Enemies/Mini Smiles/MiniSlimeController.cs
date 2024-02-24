using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class MiniSlimeController : EnemyController
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
        }

        private void FixedUpdate()
        {
            UpdatePhysic();
        }
    }
}
