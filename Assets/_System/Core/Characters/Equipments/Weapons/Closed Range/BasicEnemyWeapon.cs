using UnityEngine;
using Utils.String;

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

        public override void OnUpdate()
        {

            
        }

        protected override void OnEvent(string obj)
        {
            if (obj.Equals(StrManager.TriggerDamageEvent))
            {
                
            }
        }
    }
}
