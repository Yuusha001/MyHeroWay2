using UnityEngine;


namespace MyHeroWay
{
    public abstract class Weapon : Equipment
    {
        public bool equipWeapon;
        public bool isPrimaryWeapon;
        public float attackRange;
        public bool isActiveCombo;
        public WeaponMoveSets weaponMoveSets;
        public WeaponStats weaponStats;
        public EWeaponType weaponType;
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
            equipWeapon = true;
            controller.animatorHandle.OnEventAnimation += OnEvent;
        }
        public virtual void OnUnEquip()
        {
            equipWeapon = false;
            controller.animatorHandle.OnEventAnimation -= OnEvent;
        }
        protected abstract void OnEvent(string eventName);
    }
}