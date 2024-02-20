using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    public abstract class Controller : MonoBehaviour
    {
        [ReadOnly]
        public Core core;
        [Foldout("Stats")]
        [ReadOnly]
        public CharacterStats originalStats;
        [Foldout("Stats")]
        [ReadOnly]
        public CharacterStats runtimeStats;
        [Foldout("Animator")]
        [ReadOnly]
        [SerializeField]
        protected bool isInteracting;
        [Foldout("Animator")]
        [ReadOnly]
        [SerializeField]
        protected bool isWalking;
        [Foldout("Animator")]
        [ReadOnly]
        [SerializeField]
        protected bool isDashing;
        [Foldout("Animator")]
        [ReadOnly]
        [SerializeField]
        protected bool isActive;
        [Foldout("Animator")]
        [ReadOnly]
        public AnimatorHandle animatorHandle;

        public bool IsInteracting { get => isInteracting; set => isInteracting = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public bool IsWalking()
        {
            isWalking = !GetMovement().direction.Equals(Vector2.zero);
            return isWalking;
        }

        public Movement GetMovement()
        {
            return core.movement;
        }

        public Combat GetCombat()
        {
            return core.combat;
        }

        public EDamageSenderType GetDamageSenderType()
        {
            return core.combat.damageSenderType;
        }

        public virtual void Die(bool deactiveCharacter)
        {
            IsActive = false;
            if (deactiveCharacter)
            {
                animatorHandle.DeactiveCharacter();
            }
        }

        public virtual void ResetController()
        {
            Resume();
        }
        public virtual void SetControllerSpeed(float speed)
        {
            animatorHandle.animator.speed = speed;
        }
        public virtual void Pause()
        {
            IsActive = false;
            if (animatorHandle != null)
            {
                animatorHandle.PauseAnimator();
                core.Pause();
            }
        }
        public virtual void Resume()
        {
            if (runtimeStats.health > 0)
            {
                IsActive = true;
            }
            animatorHandle.ResumeAnimator();
            core.Resume();
        }
        [Button("GetReference")]
        private void GetReference()
        {
            animatorHandle = GetComponentInChildren<AnimatorHandle>();
            core = GetComponentInChildren<Core>();
        }
    }
}

