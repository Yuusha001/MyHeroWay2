using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class GameManager : Singleton<GameManager>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() => Application.targetFrameRate = 61;

        private void Start()
        {
            UIManager.Instance.Initialize(this);
            PopupManager.Instance.Initialize(this);
        }
    }
}