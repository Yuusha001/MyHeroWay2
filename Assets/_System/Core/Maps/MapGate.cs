using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class MapGate : MonoBehaviour
    {
        // 2 Enum Only  
        [EnumFlags]
        public EMapLink mapLink;
        public Vector2 spawnPos;

        [Button("Get Pos")]
        private void GetPos()
        {
            spawnPos = transform.GetChild(0).position;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Utils.String.StrManager.PlayerTag))
            {
                MapsManager.Instance.SetPlayerPosition(mapLink);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(this.transform.GetChild(0).position, 0.1f);
        }
    }
}
