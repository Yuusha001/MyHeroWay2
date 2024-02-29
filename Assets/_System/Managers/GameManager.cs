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

        private void OnEnable()
        {
            DataManager.Instance.LoadData();
            PlayerControlManager.Instance.Initialize();
            UIManager.Instance.Initialize(this);
            PopupManager.Instance.Initialize(this);
        }

        private void Start()
        {
           
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
        }
    }
}