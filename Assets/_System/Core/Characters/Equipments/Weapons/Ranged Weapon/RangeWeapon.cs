using MyHeroWay.SciptableObject;

namespace MyHeroWay
{
    public class RangeWeapon : Weapon
    {
        protected int numProjectile;
        protected int totalProjectile;
        protected float coolDown;
        protected float reloadTimer;
        public override void Initialize(Controller controller)
        {
            base.Initialize(controller);
            RangeWeaponStats Stats = weaponStats as RangeWeaponStats;
            totalProjectile = Stats.totalProjectile;
            coolDown = Stats.coolDown;
            numProjectile = totalProjectile;
            reloadTimer = 0;
        }

        public override void TriggerWeapon()
        {
            
        }

        protected override void OnEvent(string eventName)
        {
           
        }

        public string GetDurability()
        {
            return $"{numProjectile} / {totalProjectile}";
        }

        public float GetReloadNormalizedTime()
        {
            return reloadTimer / coolDown;
        }
    }
}
