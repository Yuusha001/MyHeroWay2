using UnityEngine;

namespace MyHeroWay
{
    public abstract class AnimatorHandle : MonoBehaviour
    {
        public Animator animator { get; protected set; }
        protected Controller controller;
        public virtual void Initialize(Controller controller)
        {
            animator = GetComponent<Animator>();
            this.controller = controller;
        }
        public abstract void ResetAnimator();
        public abstract void UpdateLogic();
        public abstract void UpdatePhysic();
        public virtual void PauseAnimator()
        {
            animator.speed = 0;
        }
        public virtual void ResumeAnimator()
        {
            animator.speed = 1;
        }
        public virtual void DeactiveCharacter()
        {
            gameObject.SetActive(false);
        }

        public void SetFloat(string parameter, float value)
        {
            animator.SetFloat(Animator.StringToHash(parameter), value);
        }
        public void SetFloat(string parameter, float value, float speedAnimation)
        {
            animator.SetFloat(Animator.StringToHash(parameter), value);
            animator.speed = speedAnimation;
        }
        public void SetBool(string parameter, bool status)
        {
            animator.SetBool(Animator.StringToHash(parameter), status);
        }
        public bool GetBool(string parameter)
        {
            return animator.GetBool(Animator.StringToHash(parameter));
        }

        public void PlayAnimation(string parameter, float normalizedTransitionDuration, int layer, bool isInteracting)
        {
            animator.CrossFade(Animator.StringToHash(parameter), normalizedTransitionDuration, layer);
            SetBool("IsInteracting", isInteracting);
        }
       
        public void PlayAnimation(string parameter, float normalizedTransitionDuration, int layer)
        {
            animator.CrossFade(Animator.StringToHash(parameter), normalizedTransitionDuration, layer);
        }
    }

}
