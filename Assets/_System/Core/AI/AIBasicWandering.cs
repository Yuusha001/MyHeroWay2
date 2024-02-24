using UnityEngine;
using UnityEngine.AI;

namespace MyHeroWay
{
    public class AIBasicWandering : AIState
    {
        EnemyController enemyController;
        NavMeshAgent navMeshAgent;
        Vector3 point;
        private bool isMoving;
        private float RestTime;
        NavMeshHit hit;
        public AIBasicWandering(EnemyController controller, string stateName) : base(controller, stateName)
        {
            this.enemyController = controller;
            navMeshAgent = enemyController.navMeshAgent;

        }

        public override void EnterState()
        {
            isMoving = false;
            RestTime = 0;
            if (enemyController.target != null)
            {
                enemyController.SwitchState(enemyController.chasingState);
            }
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateLogic()
        {
            if (enemyController.target != null)
            {
                isMoving = false;
                enemyController.SwitchState(enemyController.chasingState);
            }
            if (RestTime >= 0)
            {
                enemyController.GetMovement().SetDirection(Vector2.zero);
                RestTime -= Time.deltaTime;
                return;
            }

            if (!isMoving)
            {
                point = RandomPoint(enemyController.transform.position, enemyController.warnderingRange);
                bool isOnNavMesh = NavMesh.SamplePosition(point, out hit, 0.1f, NavMesh.AllAreas);
                if (!isOnNavMesh)
                {
                    return;
                }
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos

                //Rotate FOV
                Vector3 direction = point - enemyController.fieldOfView.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                enemyController.fieldOfView.transform.rotation = rotation;

                //Set Dir
                int x = Mathf.RoundToInt(direction.normalized.x);
                int y = Mathf.RoundToInt(direction.normalized.y);
                Vector2 roundedDirection = new Vector2(x, y);
                enemyController.enemyAnimator.SetLastDirection(roundedDirection);
                enemyController.GetMovement().SetDirection(roundedDirection);

                navMeshAgent.SetDestination(point);
                isMoving = true;
            }

            if (Vector3.Distance(enemyController.transform.position, point) <= 0.1f) //done with path
            {
                isMoving = false;
                RestTime = Random.Range(0, 3);
            }
        }

        public override void UpdatePhysic()
        {
            
        }

        Vector3 RandomPoint(Vector3 center, float range)
        {
            float x = center.x + Random.Range(-range, range);
            float y = center.y + Random.Range(-range, range);
            Vector3 randomPoint = new Vector3(x, y, center.z);
            return randomPoint;
        }
    }
}
