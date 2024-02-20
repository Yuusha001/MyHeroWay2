using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class MiniSlimeController : EnemyController
    {
        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            UpdateLogic();
        }

        private void FixedUpdate()
        {
            UpdatePhysic();
        }
    }
}
