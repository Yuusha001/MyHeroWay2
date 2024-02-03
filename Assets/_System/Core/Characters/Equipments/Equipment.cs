using UnityEngine;

namespace MyHeroWay
{
    public abstract class Equipment : MonoBehaviour
    {
        public CharacterStats equipmentStats;
        public EEquipmentType equipmentType;
        public EquipmentData data;
        public abstract void Initialize(Controller controller);
        public virtual void SetEquipmentData(EquipmentData data)
        {
            equipmentStats = new CharacterStats();
            this.data = data;
        }
        public virtual void SetUpPassive(CharacterStats originalStats)
        {

        }
        public virtual void OnUpdate()
        {

        }
    }
}