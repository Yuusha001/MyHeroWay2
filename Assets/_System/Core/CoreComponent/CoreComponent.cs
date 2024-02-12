using UnityEngine;

namespace MyHeroWay
{
    public abstract class CoreComponent : MonoBehaviour
    {
        [SerializeField]
        protected Core core;
        public virtual void Initialize(Core core)
        {
            this.core = core;
        }
    }
}

