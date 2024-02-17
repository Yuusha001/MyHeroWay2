using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace MyHeroWay.MapDeco
{
    public class CampfireDeco : MonoBehaviour
    {
        [ReadOnly]
        public Animator animator;

        [Button("Turn Smoke")]
        public void TurnSmoke()
        {
            animator.CrossFade(Animator.StringToHash("smoke"), 0.1f);
        }

        [Button("Turn Off")]
        public void TurnOff()
        {
            animator.CrossFade(Animator.StringToHash("off"), 0.1f);
        }

    }
}
