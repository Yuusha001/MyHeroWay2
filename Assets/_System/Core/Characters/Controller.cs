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

        public bool IsWalking()
        {
            isWalking = !GetMovement().direction.Equals(Vector2.zero);
            return isWalking;
        }

        public bool IsInteracting()
        {
            return isInteracting;
        }

        public bool IsActive()
        {
            return isActive;
        }

        public Movement GetMovement()
        {
            return core.movement;
        }

        public virtual void Die(bool deactiveCharacter)
        {
            isActive = false;
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
            isActive = false;
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
                isActive = true;
            }
            //animatorHandle.ResumeAnimator();
            core.Resume();
        }
    }
}

