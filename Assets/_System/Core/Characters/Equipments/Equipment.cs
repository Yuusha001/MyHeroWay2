
namespace MyHeroWay
{
    public abstract class Equipment : ItemGame
    {
        public CharacterStats equipmentStats;
        public EEquipmentType equipmentType;
        public EquipmentData equipmentData;
        public abstract void Initialize(Controller controller);
        public virtual void SetEquipmentData(EquipmentData data)
        {
            equipmentStats = new CharacterStats();
            this.equipmentData = data;
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