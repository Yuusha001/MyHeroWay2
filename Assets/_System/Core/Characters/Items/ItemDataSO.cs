using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "ItemDataSO", menuName = "Item/ItemData", order = 0)]
    public class ItemDataSO : ScriptableObject
    {
        public int id;
        public ItemGame prefab;
        [ShowAssetPreview]
        public Sprite icon;
        public EItemType itemType;
        public string itemName;
        [ResizableTextArea]
        public string description;
    }
}
