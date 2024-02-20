using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Utils
{
    public class Delay
    {
        public static void DoAction(MonoBehaviour root, System.Action action, float delay)
        {
            root.StartCoroutine(CoAction(action, delay));
        }

        private static IEnumerator CoAction(System.Action action, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (action != null)
                action();
        }

        public static async UniTask DoAction(System.Action action, float delay)
        {
            await UniTask.WaitForSeconds(delay);
            if (action != null)
                action?.Invoke();
        }

        public static CancellationToken RefreshToken(ref CancellationTokenSource tokenSource)
        {
            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = new CancellationTokenSource();
            return tokenSource.Token;
        }
    }
}