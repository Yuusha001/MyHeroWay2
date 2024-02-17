using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class UIManager : Singleton<UIManager>
    {
        private GameManager manager;
        [ReadOnly]
        public ScreenUI[] screens;
        public void Initialize(GameManager manager)
        {
            this.manager = manager;
            for (int i = 0; i < screens.Length; i++)
            {
                screens[i].Initialize(this);
            }
        }

        public T ActiveScreen<T>()
        {
            T screen = default;
            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i] is T)
                {
                    screens[i].Active();
                    screen = screens[i].GetComponent<T>();
                }
                else
                {
                    screens[i].Deactive();
                }
            }
            return screen;
        }

        public T DeactiveScreen<T>()
        {
            T screen = default;
            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i] is T)
                {
                    screens[i].Deactive();
                    screen = screens[i].GetComponent<T>();
                }
                else
                {
                    screens[i].Deactive();
                }
            }
            return screen;
        }

        [Button("Get References")]
        private void GetReferences()
        {
            screens = FindObjectsOfType<ScreenUI>(true);
        }
    }
}
