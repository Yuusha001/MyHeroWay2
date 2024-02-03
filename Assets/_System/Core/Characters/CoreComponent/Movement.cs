using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay

{
    public class Movement : CoreComponent
    {
        public Rigidbody2D rb;
        public int movementSpeed;
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
        }

        public void UpdateLogic()
        {
            currentVelecity = rb.velocity;
        }

        public void UpdatePhysic()
        {
            HandleMovement();
        }

        public void SetVelocityX(float velocityX)
        {
            workSpace.Set(velocityX, currentVelecity.y);
            SetFinalVelocity();
        }
        public void SetVelocityY(float velocityY)
        {
            workSpace.Set(currentVelecity.x, velocityY);
            SetFinalVelocity();
        }
        public void SetVelocityZero()
        {
            workSpace = Vector2.zero;
            SetFinalVelocity();
        }
        public void SetVelocity(Vector2 direction, float velocity)
        {
            workSpace = direction * velocity;
            SetFinalVelocity();
        }
        public void SetVelocity(Vector2 velocity)
        {
            workSpace = velocity;
            SetFinalVelocity();
        }
        public void SetBodyType(RigidbodyType2D type)
        {
            rb.bodyType = type;
        }
        private void SetFinalVelocity()
        {
            rb.velocity = workSpace;
            currentVelecity = workSpace;
        }

        public void SetDirection(Vector2 vector2)
        {
            direction = vector2;
            if (direction != Vector2.zero)
            {
                if (vector2.x == 1)
                {
                    facingDirection = EFacingDirection.RIGHT;
                }
                if (vector2.x == -1)
                {
                    facingDirection = EFacingDirection.LEFT;
                }
                if (vector2.y == 1)
                {
                    facingDirection = EFacingDirection.UP;
                }
                if (vector2.y == -1)
                {
                    facingDirection = EFacingDirection.DOWN;
                }
            }
        }

        private void HandleMovement()
        {
            if (core.controller.IsInteracting || !core.controller.IsActive) return;
            //transform.position += direction * movementSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + direction * (movementSpeed * Time.fixedDeltaTime));
        }
    }
}

