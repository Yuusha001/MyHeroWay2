using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class EnemyAnimator : AnimatorHandle
    {
        public override void Initialize(Controller controller)
        {
            base.Initialize(controller);
        }

        public override void ResetAnimator()
        {
            
        }

        public override void UpdateLogic()
        {
            controller.IsInteracting = GetBool(StrManager.isInteracting);

        }

        public override void UpdatePhysic()
        {
            SetBool(StrManager.isWalking, controller.IsWalking());
        }

        public void SetLastDirection(Vector2 lastDir)
        {
            SetFloat(StrManager.moveX, lastDir.x);
            SetFloat(StrManager.moveY, lastDir.y);
        }
    }
}
