using UnityEngine;

namespace MyHeroWay
{
    public abstract class AIState
    {
        public string stateName;
        protected Controller controller;
        protected Core core;
        protected Transform transform;
        public AIState(Controller controller, string stateName)
        {
            this.controller = controller;
            this.stateName = stateName;
            this.core = controller.core;
            this.transform = controller.transform;
        }
        public abstract void EnterState();
        public abstract void UpdateLogic();
        public abstract void UpdatePhysic();
        public abstract void ExitState();
    }
}
