using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyHeroWay
{
    public class AIBasicAttacking : AIState
    {
        EnemyController enemyController;
        NavMeshAgent navMeshAgent;
        public AIBasicAttacking(EnemyController controller, string stateName) : base(controller, stateName)
        {
            this.enemyController = controller;
            this.navMeshAgent = enemyController.navMeshAgent;
        }

        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateLogic()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdatePhysic()
        {
            throw new System.NotImplementedException();
        }
    }
}
