using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace MyHeroWay
{
    public class PlayerController : Controller
    {
        public CharacterAnimator playerAnimator;
        public CharacterEquipment characterEquipment;
        public void Initialize(PlayerControlManager playerControlManager)
        {
            core.Initialize(this);
            animatorHandle = playerAnimator;
            playerAnimator.Initialize(this);
            characterEquipment.Initialize(this, playerAnimator);
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
            GetMovement().SetDirection(vector2);
            if (vector2 != Vector2.zero)
            {
                playerAnimator.SetLastDirection(vector2);
            }
        }
    }
}
