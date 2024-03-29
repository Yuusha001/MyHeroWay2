using MyHeroWay.Skills;
using MyHeroWay.Stats;
using NaughtyAttributes;
using UnityEngine;
using Utils;

namespace MyHeroWay
{
    public class PlayerController : Controller
    {
        public PlayerControls playerInput;
        [Foldout("Animator")]
        public CharacterAnimator playerAnimator;
        public CharacterEquipment characterEquipment;
        public SkillTree playerSkillTree;
        [ReadOnly]
        public CharacterExp characterExp;
        public CharacterStatsModifier playerStatsModifier;
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
            GetCombat().OnStatsChange += StatsChangeHandler;
            CaculateStats();
            playerSkillTree.Initialize(this);
        }

        public void HandlePrimaryAttack()
        {
            if (!isActive) return;
            characterEquipment.HandlePrimaryAttack();
        }

        public void HandleSecondaryAttack()
        {
            if (!isActive) return;
            characterEquipment.HandleSecondaryAttack();
        }

        public void HandleSkills(int number)
        {
            Debug.Log(number);
        }

        public void HandleDash()
        {
            if (!isActive) return;
            if (isDashing) return;
            Dash dash = playerSkillTree.GetSkill<Dash>();
            if (dash.CanUseSkill())
            {
                isDashing = true;
                dash.UseSkill(() => isDashing = false);
            }
        }

        public void UpdateScript()
        {
            if (!isActive) return;
            SetMovementDirection(playerInput.Movement.Move.ReadValue<Vector2>());
            core.UpdateLogic();
            playerAnimator.UpdateLogic();
            characterEquipment.UpdateLogic();
            playerSkillTree.UpdateLogic();
        }

        public void FixedUpdateScript()
        {
            if (!isActive) return;
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

        private void StatsChangeHandler()
        {
            if (runtimeStats.health == 0 && isActive)
            {
                animatorHandle.PlayAnimation("Die", 0.1f, 0);
                GetMovement().SetVelocityZero();
                Die(false);
            }
        }

        [Button("Caculate Stats")]
        private void CaculateStats()
        {
            var Data = DataManager.Instance.data;
            characterExp = new CharacterExp(Data.userLevel, Data.userEXP);
            int currentLevel = characterExp.CurrentLevel;
            originalStats = StatsCaculation.GetFinalCharacterStats(currentLevel, playerStatsModifier);
            runtimeStats = new CharacterStats() + StatsCaculation.GetFinalCharacterStats(currentLevel, playerStatsModifier);
            runtimeStats.level = currentLevel;
        }

        public void AddEXP(int exp)
        {
            characterExp.AddEXP(exp,
                () => DataManager.Instance.LevelUp()
            );
            DataManager.Instance.AddExp(characterExp);
        }
    }
}
