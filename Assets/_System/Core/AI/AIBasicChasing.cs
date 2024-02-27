using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

namespace MyHeroWay
{
    public class AIBasicChasing : AIState
    {
        EnemyController enemyController;
        NavMeshAgent navMeshAgent;
        Controller target;
        AIBehavior behavior;

        public AIBasicChasing(EnemyController controller, string stateName) : base(controller, stateName)
        {
            this.enemyController = controller;
            this.navMeshAgent = enemyController.navMeshAgent;
            this.target = enemyController.target;
            behavior = new AIBehavior(enemyController, navMeshAgent, enemyController.fieldOfView);
            behavior.enemyTpye = controller.Type;
        }

        public override void EnterState()
        {
            if (target != null)
            {
                navMeshAgent.SetDestination(target.transform.position);
            }
        }

        public override void ExitState()
        {
            enemyController.GetCombat().isInvincible = false;
        }

        public override void UpdateLogic()
        {
            if (enemyController.target != null && !enemyController.target.IsActive)
            {
                enemyController.target = null;
                enemyController.SwitchState(enemyController.wanderingState);
            }
            switch (enemyController.AIBehavior)
            {
                case EAIBehavior.Hunting:
                    behavior.Hunt();
                    break;
                case EAIBehavior.RunToAlly:
                    behavior.RunToAlly();
                    break;
                case EAIBehavior.CallForReinforcements:
                    behavior.CallForReinforcements();
                    break;
                case EAIBehavior.Shooting:
                    behavior.ComeNearby();
                    break;
            }
        }

        public override void UpdatePhysic()
        {
            
        }
    }
}
