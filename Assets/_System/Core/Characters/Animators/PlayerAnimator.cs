namespace MyHeroWay
{
    using Utils.String;

    public class PlayerAnimator : AnimatorHandle
    {
        private UnityEngine.Vector2 dir;
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
    }
}