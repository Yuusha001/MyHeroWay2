namespace MyHeroWay
{
    public class BasicEnemyWeapon : Weapon
    {
        public override void Initialize(Controller controller)
        {
            base.Initialize(controller);
            this.weaponType = EWeaponType.Pole;
        }
        public override void TriggerWeapon()
        {
            
        }

        protected override void OnEvent(string eventName)
        {
            
        }
    }
}
