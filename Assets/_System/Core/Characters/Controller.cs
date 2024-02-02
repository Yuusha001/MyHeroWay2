using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public abstract class Controller : MonoBehaviour
    {
        public Core core;
        public CharacterStats originalStats;
        public CharacterStats runtimeStats;
        public bool isInteracting;
        public bool isActive;
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

