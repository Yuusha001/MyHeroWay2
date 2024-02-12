using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public abstract class AnimatorHandle : MonoBehaviour
    {
        public Animator animator { get; protected set; }
        protected Controller controller;
        public event System.Action<string> OnEventAnimation;
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

        public void SetTrigger(string parameter)
        {
            controller.IsInteracting = true;
            SetBool(StrManager.isInteracting, controller.IsInteracting);
            animator.SetTrigger(Animator.StringToHash(parameter));
        }

        public void PlayAnimation(string parameter, float normalizedTransitionDuration, int layer, bool isInteracting)
        {
            animator.CrossFade(Animator.StringToHash(parameter), normalizedTransitionDuration, layer);
            SetBool(StrManager.isInteracting, isInteracting);
        }
       
        public void PlayAnimation(string parameter, float normalizedTransitionDuration, int layer)
        {
            animator.CrossFade(Animator.StringToHash(parameter), normalizedTransitionDuration, layer);
        }
        public void SendEvent(string eventName)
        {
            OnEventAnimation?.Invoke(eventName);
        }
    }

}
