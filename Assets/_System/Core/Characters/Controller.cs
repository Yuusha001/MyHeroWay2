using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public abstract class Controller : MonoBehaviour
    {
        [ReadOnly]
        public Core core;
        [ReadOnly]
        public CharacterStats originalStats;
        [ReadOnly]
        public CharacterStats runtimeStats;
        [ReadOnly]
        [SerializeField]
        private bool isInteracting;
        [ReadOnly]
        [SerializeField]
        private bool isWalking;
        [ReadOnly]
        [SerializeField]
        private bool isRunning;
        [ReadOnly]
        [SerializeField]
        private bool isActive;
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

        public virtual void Die(bool deactiveCharacter)
        {
            IsActive = false;
            if (deactiveCharacter)
            {
                //animatorHandle.DeactiveCharacter();
            }
        }

        public virtual void ResetController()
        {
            Resume();
        }
        public virtual void SetControllerSpeed(float speed)
        {
            //animatorHandle.animator.speed = speed;
        }
        public virtual void Pause()
        {
            IsActive = false;
            /*if (animatorHandle != null)
            {
                animatorHandle.PauseAnimator();
                core.Pause();
            }*/
        }
        public virtual void Resume()
        {
            if (runtimeStats.health > 0)
            {
                IsActive = true;
            }
            //animatorHandle.ResumeAnimator();
            core.Resume();
        }
    }
}

