using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    public abstract class ItemGame : MonoBehaviour
    {
        public EItemType itemType;
        public ItemData data;
        [Expandable]
        public ItemDataSO dataSO;
    }
}
