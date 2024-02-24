using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay

{
    public class Movement : CoreComponent
    {
        public Rigidbody2D rb;
        public float movementSpeed;
        public Vector2 currentVelecity;
        public Vector2 workSpace;
        public Vector2 direction;
        [EnumFlags]
        public EFacingDirection facingDirection;
       
        public override void Initialize(Core core)
        {
            base.Initialize(core);
            facingDirection = EFacingDirection.DOWN;
            rb = core.controller.GetComponent<Rigidbody2D>();
            movementSpeed = Stats.StatsCaculation.GetRealSpeedByStats(core.controller.runtimeStats.speed);
        }

        public void UpdateLogic()
        {
            currentVelecity = rb.velocity;
        }

        public void UpdatePhysic()
        {
            HandleMovement();
        }
        public void SetVelocityZero()
        {
            workSpace = Vector2.zero;
            SetFinalVelocity();
        }
        private void SetFinalVelocity()
        {
            rb.velocity = workSpace;
            currentVelecity = workSpace;
        }
        public void AddForce(Vector2 force)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        public void SetDirection(Vector2 vector2)
        {
            direction = vector2;
            if (Vector2.Dot(direction, Vector2.up) > 0.9f)
            {
                facingDirection = EFacingDirection.UP;
            }
            else if (Vector2.Dot(direction, -Vector2.up) > 0.9f)
            {
                facingDirection = EFacingDirection.DOWN;
            }
            else if (Vector2.Dot(direction, Vector2.left) > 0.9f)
            {
                facingDirection = EFacingDirection.LEFT;
            }
            else if (Vector2.Dot(direction, Vector2.right) > 0.9f)
            {
                facingDirection = EFacingDirection.RIGHT;
            }
            HanderFlip();
        }

        private void HanderFlip()
        {
            if (core.controller is PlayerController) return;
            if (direction.x == 0) return;
            core.controller.animatorHandle.transform.localScale = new Vector3(-1*direction.x, 1, 1);
        }

        private void HandleMovement()
        {
            if (core.controller is EnemyController) return;
            if (core.controller.IsInteracting || !core.controller.IsActive || core.combat.isStunning) return;
            //transform.position += direction * movementSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + direction * (movementSpeed * Time.fixedDeltaTime));
        }
    }
}

