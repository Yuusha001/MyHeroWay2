using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class PlayerController : Controller
    {
        public PlayerAnimator playerAnimator;
        public void Initialize(PlayerControlManager playerControlManager)
        {
            core.Initialize(this);
            playerAnimator.Initialize(this);
        }
        
        public void UpdateScript()
        {
            core.UpdateLogic();
            playerAnimator.UpdateLogic();
        }

        public void FixedUpdateScript()
        {
            core.UpdatePhysic();
            playerAnimator.UpdatePhysic();
        }

        public void SetMovementDirection(Vector2 vector2)
        {
            GetMovement().direction = vector2;
        }
    }
}
