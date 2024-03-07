using UnityEngine;

namespace MyHeroWay
{
    public abstract class PopupUI : MonoBehaviour
    {
        protected PopupManager _popupManager;
        protected System.Action _onClosed;
        public virtual void Initialize(PopupManager popupManager, System.Action onClosed = null)
        {
            _popupManager = popupManager;
            _onClosed = onClosed;
        }
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        public virtual void Close()
        {
            _onClosed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}