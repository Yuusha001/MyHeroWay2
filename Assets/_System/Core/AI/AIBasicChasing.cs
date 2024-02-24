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
            behavior = new AIBehavior();
            behavior.enemyController = controller;
            behavior.fov = enemyController.fieldOfView;
            behavior.navMeshAgent = navMeshAgent;
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
            }
        }

        public override void UpdatePhysic()
        {
            
        }
    }
}
