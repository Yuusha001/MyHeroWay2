using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
            if(Vector3.Distance(enemyController.target.transform.position, enemyController.transform.position) <= enemyController.weapon.attackRange)
            {
                enemyController.weapon.TriggerWeapon();
            }
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateLogic()
        {
            if(enemyController.target == null)
            {
                enemyController.SwitchState(enemyController.wanderingState);
            }


            if (Vector3.Distance(enemyController.target.transform.position, enemyController.transform.position) <= enemyController.weapon.attackRange)
            {
                enemyController.weapon.TriggerWeapon();
            }
            else
            {
                enemyController.SwitchState(enemyController.chasingState);
            }
        }

        public override void UpdatePhysic()
        {
            
        }

        public void FocusFOVonPlayer()
        {
            if (enemyController.target == null) return;
            //Rotate FOV
            Vector3 direction = enemyController.target.transform.position - enemyController.fieldOfView.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            enemyController.fieldOfView.transform.rotation = rotation;

        }
    }
}
