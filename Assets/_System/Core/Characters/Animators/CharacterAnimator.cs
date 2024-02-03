namespace MyHeroWay
{
    using UnityEngine;
    using Utils.String;

    public class CharacterAnimator : AnimatorHandle
    {
        private Vector2 dir;
        public override void Initialize(Controller controller)
        {
            base.Initialize(controller);
        }
        public override void ResetAnimator()
        {
            
        }

        public override void UpdateLogic()
        {
            
        }

        public override void UpdatePhysic()
        {
            dir = controller.GetMovement().direction;
            SetBool(StringManager.isWalking, controller.IsWalking());
            SetFloat(StringManager.moveX, dir.x);
            SetFloat(StringManager.moveY, dir.y);
        }

        public void SetLastDirection(Vector2 lastDir)
        {
            SetFloat(StringManager.lastMoveX, lastDir.x);
            SetFloat(StringManager.lastMoveY, lastDir.y);
        }
    }
}