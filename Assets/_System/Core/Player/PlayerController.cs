using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class PlayerController : Controller
    {
        public void Initialize(PlayerControlManager playerControlManager)
        {
            core.Initialize(this);
        }
        
        public void UpdateScript()
        {
            core.UpdateLogicCore();
        }

        public void FixedUpdateScript()
        {
            core.UpdatePhysicCore();
        }

        public void SetMovementDirection(Vector2 vector2)
        {
            core.movement.direction = vector2;
        }
    }
}
