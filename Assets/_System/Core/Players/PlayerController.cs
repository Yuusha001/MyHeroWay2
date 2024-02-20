using UnityEngine;
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
            playerInput.Combat.SecondaryWeapon.started += _ => HandleSecondaryAttack();
            playerInput.Movement.Dash.started += _ => HandleDash();
            playerInput.Movement.Move.performed += callback => SetLastDirection(callback.ReadValue<Vector2>());
            playerInput.Skills.Keyboard.performed += callback => HandleSkills((int)callback.ReadValue<float>());
            playerAnimator.SetLastDirection(Vector2.down);
        }

        public void HandlePrimaryAttack()
        {
            characterEquipment.HandlePrimaryAttack();
        }

        public void HandleSecondaryAttack()
        {
            characterEquipment.HandleSecondaryAttack();
        }

        public void HandleSkills(int number)
        {
            Debug.Log(number);
        }

        public async void HandleDash()
        {
            if (isDashing) return;
            isDashing = true;
            GetMovement().movementSpeed *= 4;
            await Delay.DoAction(() =>
            {
                GetMovement().movementSpeed /= 4;
                isDashing = false;
            }, 0.3f);
        }

        public void UpdateScript()
        {
            SetMovementDirection(playerInput.Movement.Move.ReadValue<Vector2>());
            core.UpdateLogic();
            playerAnimator.UpdateLogic();
            characterEquipment.UpdateLogic();
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
