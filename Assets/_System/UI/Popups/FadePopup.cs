using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class FadePopup : PopupUI
    {
        [SerializeField] 
        private Image fadeScreen;
        [SerializeField] 
        private float fadeSpeed = 1f;
        private CancellationTokenSource _cancellationTokenSource;
        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager, onClosed);
        }

        public override void Show()
        {
            base.Show(); 
            FadeToClear();
        }

        public override void Close()
        {
            FadeToBlack();
            base.Close();
        }
        
        [Button("Fade in")]
        public async void FadeToBlack()
        {
            await FadeRoutine(1, Utils.Delay.RefreshToken(ref _cancellationTokenSource));
        }
        [Button("Fade out")]
        public async void FadeToClear()
        {
           await  FadeRoutine(0, Utils.Delay.RefreshToken(ref _cancellationTokenSource));
        }

        private async UniTask FadeRoutine(float targetAlpha, CancellationToken token)
        {
            while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
            {
                float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
                await UniTask.Yield(token);
            }
        }   
    }
}
