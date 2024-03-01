using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MyHeroWay
{
    public class CameraManager : Singleton<CameraManager>
    {
        public CinemachineImpulseSource _impulseSource;

        private void Start()
        {
            PlayerControlManager.Instance.playerController.GetCombat().OnGetHit += ShakeScreen;
        }

        public void ShakeScreen()
        {
            _impulseSource.GenerateImpulse();
        }
    }
}
