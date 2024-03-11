using UnityEngine;
using NaughtyAttributes;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "ItemContainer", menuName = "Item/ItemContainer", order = 2)]
    public class ItemContainer : ScriptableObject
    {
        public ItemDataSO[] container;
        public ItemDataSO GetItemObject(int id)
        {
            foreach (var item in container)
            {
                if (item.id == id)
                    return item;
            }
            return null;
        }

        [Button("GenerateID")]
        private void GenerateID()
        {
            for (int i = 0; i < container.Length; i++)
            {
                container[i].id = i + 1;
            }
        }
    }
}
