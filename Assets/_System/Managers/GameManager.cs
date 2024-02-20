using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class GameManager : Singleton<GameManager>
    {

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            UIManager.Instance.Initialize(this);
            PopupManager.Instance.Initialize(this);
        }
    }
}