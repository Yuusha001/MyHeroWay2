using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class PopupManager : Singleton<PopupManager>
    {
        private GameManager manager;
        [ReadOnly]
        public PopupUI[] popups;
        public void Initialize(GameManager manager)
        {
            this.manager = manager;
            for (int i = 0; i < popups.Length; i++)
            {
                popups[i].Initialize(this);
            }
        }

        public T GetPopup<T>()
        {
            T popup = default;
            for (int i = 0; i < popups.Length; i++)
            {
                if (popups[i] is T)
                {
                    popup = popups[i].GetComponent<T>();
                }
            }
            return popup;
        }
        public T ShowPopup<T>()
        {
            T popup = default;
            for (int i = 0; i < popups.Length; i++)
            {
                if (popups[i] is T)
                {
                    popups[i].Show();
                    popup = popups[i].GetComponent<T>();
                }
            }
            return popup;
        }
        public T ClosePopup<T>()
        {
            T popup = default;
            for (int i = 0; i < popups.Length; i++)
            {
                if (popups[i] is T)
                {
                    popups[i].Close();
                    popup = popups[i].GetComponent<T>();
                }
            }
            return popup;
        }

        [Button("Get All Popups")]
        private void GetAllPopups()
        {
            popups = FindObjectsOfType<PopupUI>(true);
        }
    }
}
