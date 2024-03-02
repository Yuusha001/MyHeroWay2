using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyHeroWay
{
    public class AIBehavior
    {

        public EnemyController enemyController;

        public List<EnemyController> reinforcements;

        public Controller target;

        public EnemyController nearAlly;

        public NavMeshAgent navMeshAgent;

        public FOV fov;

        public EEnemyType enemyTpye;

        private NavMeshHit hit;

        public AIBehavior(EnemyController enemyController, NavMeshAgent navMeshAgent, FOV fov)
        {
            this.enemyController = enemyController;
            this.navMeshAgent = navMeshAgent;
            this.fov = fov;
        }
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

            HandleFlip(target.transform);
            FocusFOVonPlayer();
            enemyController.GoToPosition(point);
            if (Vector3.Distance(enemyController.transform.position, point) <= 0.1f)
            {
                if (enemyController.attackingState != null)
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

        public void FocusFOVonTarget(Transform _target)
        {
            if (_target == null) return;
            //Rotate FOV
            Vector3 direction = _target.transform.position - enemyController.fieldOfView.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            enemyController.fieldOfView.transform.rotation = rotation;
        }


        public void HandleFlip(Transform _target)
        {
            if (_target == null) return;
            Vector3 direction = _target.transform.position - enemyController.fieldOfView.transform.position;
            //Set Dir
            int x = Mathf.RoundToInt(direction.normalized.x);
            int y = Mathf.RoundToInt(direction.normalized.y);
            Vector2 roundedDirection = new Vector2(x, y);
            enemyController.enemyAnimator.SetLastDirection(roundedDirection);
            enemyController.GetMovement().SetDirection(roundedDirection);
        }

        public void RunToAlly()
        {
            // Ensure there are reinforcements available
            if (fov.reinforcements.Count == 0)
            {
                Hunt();
                return;
            }

            // Initialize variables
            nearAlly = null;
            float shortestDistance = Mathf.Infinity;

            // Find the nearest reinforcement
            foreach (var ally in fov.reinforcements)
            {
                float distance = Vector3.Distance(ally.transform.position, enemyController.transform.position);
                if (distance < shortestDistance)
                {
                    nearAlly = ally;
                    shortestDistance = distance;
                }
            }

            // Ensure a nearest reinforcement is found
            if (nearAlly == null)
            {
                Debug.LogError("No nearest ally found.");
                return;
            }
            // Run towards the nearest reinforcement
            if (Vector3.Distance(enemyController.transform.position, nearAlly.transform.position) <= 0.5f)
            {
                enemyController.SwitchState(enemyController.wanderingState);
                nearAlly.target = target; // Assuming target is a Transform
            }
            else
            {
                HandleFlip(nearAlly.transform);
                FocusFOVonTarget(nearAlly.transform);
                enemyController.GoToPosition(nearAlly.transform.position);
            }
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

        public void ComeNearby()
        {
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

            HandleFlip(target.transform);
            FocusFOVonPlayer();

            enemyController.GoToPosition(point);
            if (Vector3.Distance(enemyController.transform.position, point) <= enemyController.weapon.attackRange)
            {
                if (enemyController.attackingState != null)
                    enemyController.SwitchState(enemyController.attackingState);
            }
        }

        
    }
}
