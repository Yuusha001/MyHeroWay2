using MyHeroWay.SciptableObject;

namespace MyHeroWay
{
    public class RangeWeapon : Weapon
    {
        public int numProjectile;
        protected float coolDown;
        protected int totalProjectile;
        private float reloadTimer;

        public float ReloadTimer { get => reloadTimer; protected set => reloadTimer = value; }

        public override void Initialize(Controller controller)
        {
            base.Initialize(controller);
            RangeWeaponStatsSO Stats = weaponStats as RangeWeaponStatsSO;
            totalProjectile = Stats.totalProjectile;
            coolDown = Stats.coolDown;
            numProjectile = totalProjectile;
            ReloadTimer = 0;
        }

        public override void TriggerWeapon()
        {
            
        }

        protected override void OnEvent(string eventName)
        {
           
        }

        public string GetDurability()
        {
            return $"{numProjectile}/{totalProjectile}";
        }

        public float GetReloadNormalizedTime()
        {
            return ReloadTimer / coolDown;
        }
    }
}
