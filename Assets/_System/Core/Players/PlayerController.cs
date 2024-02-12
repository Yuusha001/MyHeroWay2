using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace MyHeroWay
{
    public class PlayerController : Controller
    {
        public CharacterAnimator playerAnimator;
        public CharacterEquipment characterEquipment;
        public PlayerControls playerInput;
        public void Initialize(PlayerControlManager playerControlManager)
        {
            core.Initialize(this);
            animatorHandle = playerAnimator;
            playerAnimator.Initialize(this);
            characterEquipment.Initialize(this, playerAnimator);
            playerInput = playerControlManager.playerInput;
            playerInput.Combat.PrimaryWeapon.started += _ => HandlePrimaryAttack();
            playerInput.Movement.Dash.started += _ => HandleDash();
            
            playerInput.Movement.Move.performed += callback => SetLastDirection(callback.ReadValue<Vector2>());
            playerAnimator.SetLastDirection(Vector2.down);
        }

        public void HandlePrimaryAttack()
        {
            characterEquipment.primaryWeapon.TriggerWeapon();
        }
        
        public void HandleDash()
        {
            GetMovement().movementSpeed *= 4;
            _ = Delay.DoAction(() =>
            {
                GetMovement().movementSpeed /= 4;
            }, 0.3f);
        }

        public void UpdateScript()
        {
            SetMovementDirection(playerInput.Movement.Move.ReadValue<Vector2>());
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
        }

        public void SetLastDirection(Vector2 vector2)
        {
            if (vector2 == Vector2.zero) return;
            if (vector2 == Vector2.up || vector2 == Vector2.down  
                || vector2 == Vector2.left || vector2 == Vector2.right)
            {
                playerAnimator.SetLastDirection(vector2);
            }
            
        }
    }
}
