using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class FOV : MonoBehaviour
    {
        public float viewRadius = 5;
        public float viewAngle = 115;
        public EnemyController enemyController;

        public LayerMask obstacleMask, detectionMask;

        public List<Collider2D> targetsInRadius;

        public List<Controller> visibleTargets = new List<Controller>();

        public List<EnemyController> reinforcements = new List<EnemyController>();

        public void Initialize(EnemyController _enemyController)
        {
            enemyController = _enemyController;
        }

        public void UpdateLogic()
        {
            FindVisibleTargets();
            FindEeinforcements();
            if (visibleTargets.Count > 0)
                enemyController.target = visibleTargets[0];
        }


        void FindEeinforcements()
        {
            reinforcements.Clear();
            var colliders = Physics2D.OverlapCircleAll(transform.position,
               enemyController.reinforcementRange,
               detectionMask,
               -Mathf.Infinity,
               Mathf.Infinity).ToList();
            if (colliders.Count > 0)
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    if (!colliders[i].transform.CompareTag(StrManager.EnemyTag))
                    {
                        colliders.Remove(colliders[i]);
                        continue;
                    }
                    if (colliders[i].TryGetComponent(out EnemyController controller))
                    {
                        if (controller.Type == enemyController.Type)
                            reinforcements.Add(controller);
                    }
                }
            }
        }

        void FindVisibleTargets()
        {
            targetsInRadius = Physics2D.OverlapCircleAll(transform.position,
                viewRadius,
                detectionMask,
                -Mathf.Infinity,
                Mathf.Infinity).ToList();

            visibleTargets.Clear();

            for (int i = 0; i < targetsInRadius.Count; i++)
            {
                if (targetsInRadius[i].transform.CompareTag(StrManager.EnemyTag))
                {
                    targetsInRadius.Remove(targetsInRadius[i]);
                    continue;
                }
                Transform target = targetsInRadius[i].transform;

                Vector2 dirTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);

                Vector2 dir = new Vector2();
                dir = transform.right;

                if (Vector2.Angle(dirTarget, dir) < viewAngle / 2)
                {
                    float distanceTarget = Vector2.Distance(transform.position, target.position);
                    if (!Physics2D.Raycast(transform.position, dirTarget, distanceTarget, obstacleMask))
                    {
                        if (target.TryGetComponent(out Controller controller))
                        {
                            visibleTargets.Add(controller);
                        }
                    }
                }
            }
        }

        public Vector2 DirFromAngle(float angleDeg, bool global)
        {
            if (!global)
            {
                angleDeg += transform.eulerAngles.z;
            }
            return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
        }
    }
}