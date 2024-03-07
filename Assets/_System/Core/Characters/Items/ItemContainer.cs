using UnityEngine;
using System.Linq;
using NaughtyAttributes;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "ItemContainer", menuName = "Item/ItemContainer", order = 2)]
    public class ItemContainer : ScriptableObject
    {
        public ItemDataSO[] container;
        public ItemDataSO GetItemObject(int id)
        {
            return container.FirstOrDefault(container => container.id == id);
        }

        [Button("GenerateID")]
        private void GenerateID()
        {
            for (int i = 0; i < container.Length; i++)
            {
                container[i].id = i + 1;
                container[i].prefab.data.itemID = i + 1;
            }
        }
    }
}
