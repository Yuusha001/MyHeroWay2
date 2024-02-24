using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyHeroWay
{
    public class AIBehavior: MonoBehaviour
    {

        public EnemyController enemyController;

        public List<EnemyController> reinforcements;

        public Controller target;

        public NavMeshAgent navMeshAgent;

        public FOV fov;

        public EEnemyType enemyTpye;

        private NavMeshHit hit;

        public void Hunt()
        {
            Debug.Log("Hunting");
            target = enemyController.target;
            if (target == null)
            {
                enemyController.SwitchState(enemyController.wanderingState);
                return;
            }
            Vector3 point = target.transform.position;
            bool isOnNavMesh = NavMesh.SamplePosition(point, out hit, 0.1f, NavMesh.AllAreas);
            if (!isOnNavMesh)
            {
                enemyController.target = null;
            }
            Debug.DrawRay(point, Vector3.up, Color.red, 1.0f); //so you can see with gizmos

            FocusFOVonPlayer();

            GoToPosition(point);
            if (Vector3.Distance(enemyController.transform.position, point) <= 0.1f)
            {
                enemyController.SwitchState(enemyController.attackingState);
            }
            
        }

        public void FocusFOVonPlayer()
        {
            /*Vector2 playerPos = presas[0].transform.position - transform.position;
            float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward),
                lookToPlayerSpeed * Time.deltaTime);*/
            if (target == null) return; 
            //Rotate FOV
            Vector3 direction = target.transform.position - enemyController.fieldOfView.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            enemyController.fieldOfView.transform.rotation = rotation;

        }

        public void RunToAlly()
        {
            reinforcements = fov.reinforcements;
            if (reinforcements.Count == 0)
            {
                Hunt();
                return;
            }
            var near = reinforcements[0];
            foreach (var enemy in reinforcements)
            {
                if (Vector3.Distance(enemy.transform.position, enemyController.transform.position) <= Vector3.Distance(near.transform.position, enemyController.transform.position))
                {
                    near = enemy;
                }
            }
            Vector2 posAliado = new Vector2(near.transform.position.x, near.transform.position.y - 1);
            GoToPosition(posAliado);
            FocusFOVonPlayer();
            near.target = target;
        }

        public void CallForReinforcements()
        {
            reinforcements = fov.reinforcements;
            if (reinforcements.Count == 0)
            {
                Hunt();
                return;
            }
            FocusFOVonPlayer();
            RunToAlly();
            foreach (var item in reinforcements)
            {
                item.target = target;
            }
        }
        public void GoToPosition(Vector2 position)
        {
            navMeshAgent.SetDestination(position);
        }

        public void GoToPosition(Transform target)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }
}
