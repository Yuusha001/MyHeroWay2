using UnityEngine;
namespace MyHeroWay
{
    public class CollisionSenses : CoreComponent
    {
        [Header("GroundCheck")]
        public float footOffset = 0.2f;
        public float groundRayDistance = 0.2f;
        public LayerMask groundLayer;
        public Vector2 groundAheadRayOffset;
        public Vector2 wallRayOffset;
        public float groundCheckLength = 1;
       
        RaycastHit2D RayCast(Vector2 offset, Vector2 direction, float distance, LayerMask layerMask)
        {
            RaycastHit2D raycast = Physics2D.Raycast((Vector2)core.controller.transform.position + offset, direction, distance, layerMask);
#if UNITY_EDITOR
            Color color = raycast ? Color.red : Color.green;
            Debug.DrawRay((Vector2)core.controller.transform.position + offset, direction * distance, color);
#endif

            return raycast;
        }
        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
#if UNITY_EDITOR
                Gizmos.DrawRay(transform.position - new Vector3(footOffset, 0), Vector3.down * groundCheckLength);
                Gizmos.DrawRay(transform.position + new Vector3(footOffset, 0), Vector3.down * groundCheckLength);



#endif
                int direction = 1;
                Vector3 offsetDown = new Vector3(groundAheadRayOffset.x * direction, groundAheadRayOffset.y);
#if UNITY_EDITOR
                // Gizmos.DrawRay(new Vector2(wallRayOffset.x * -direction, wallRayOffset.y), Vector2.left * direction);
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position + new Vector3(wallRayOffset.x * direction, wallRayOffset.y), Vector2.left * direction);
                Gizmos.DrawRay(transform.position + offsetDown, Vector2.down);

#endif
            }
        }
    }
}

