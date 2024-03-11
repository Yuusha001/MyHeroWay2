using Cysharp.Threading.Tasks;
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

        private async void OnEnable()
        {
            UniTask LoadData = Utils.Delay.DoAction(() => DataManager.Instance.LoadData(), 0);
            await UniTask.WhenAll(LoadData);
           
            PlayerControlManager.Instance.Initialize();
            UIManager.Instance.Initialize(this);
            PopupManager.Instance.Initialize(this);

            InventoryManager.Instance.Initialize(DataManager.Instance.data);
            MapsManager.Instance.Initialize(DataManager.Instance.data);
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