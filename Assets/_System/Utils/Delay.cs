using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
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
            action?.Invoke();

        }

        public static async UniTask DoAction(System.Action action, float delay)
        {
            await UniTask.WaitForSeconds(delay);
            action?.Invoke();
        }

        public static CancellationToken RefreshToken(ref CancellationTokenSource tokenSource)
        {
            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = new CancellationTokenSource();
            return tokenSource.Token;
        }

        public static async UniTask<T> DoAction<T>(System.Func<T> action, float delay = 0)
        {
            await UniTask.WaitForSeconds(delay);
            return action();
        }
    }
}