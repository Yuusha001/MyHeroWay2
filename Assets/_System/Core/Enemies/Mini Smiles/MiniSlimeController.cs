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
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void UpdatePhysic()
        {
            base.UpdatePhysic();
        }

        public override void Die(bool deactiveCharacter)
        {
            base.Die(deactiveCharacter);
        }

        
    }
}
