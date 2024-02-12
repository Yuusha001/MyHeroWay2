using UnityEngine;


namespace MyHeroWay
{
    public abstract class Weapon : Equipment
    {
        public float attackRange;
        public bool isActiveCombo;
        public WeaponMoveSets weaponMoveSets;
        public WeaponStats weaponStats;
        [SerializeField]
        protected Controller controller;
        public override void Initialize(Controller controller)
        {
            this.controller = controller;
            equipmentType = EEquipmentType.WEAPON;
        }
        public abstract void TriggerWeapon();
        public virtual void OnEquip()
        {
            controller.animatorHandle.OnEventAnimation += OnEvent;
        }
        public virtual void OnUnEquip()
        {
            controller.animatorHandle.OnEventAnimation -= OnEvent;
        }
        protected abstract void OnEvent(string eventName);
    }
}