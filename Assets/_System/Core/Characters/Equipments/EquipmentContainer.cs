using UnityEngine;
using System.Linq;
using NaughtyAttributes;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "EquipmentContainer", menuName = "Item/EquipmentContainer", order = 3)]
    public class EquipmentContainer : ScriptableObject
    {
        public EquipmentDataSO[] container;
        public EquipmentDataSO GetEquipmentObject(int id)
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
