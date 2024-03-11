using UnityEngine;
using NaughtyAttributes;

namespace MyHeroWay
{
    public class MapGame : MonoBehaviour
    {
        public EMapLink mapName;
        public MapGate[] gateList;
        public Collider2D border;

        [Button("GetReference")]
        private void GetReference()
        {
            gateList = GetComponentsInChildren<MapGate>();
            border = GetComponent<Collider2D>();
            name = mapName.ToString();
        }

        public MapGate GetMapGate(EMapLink mapLink)
        {
            foreach (var item in gateList)
            {
                if ((item.mapLink & mapLink) != 0)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
