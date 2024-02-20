using UnityEngine;

namespace MyHeroWay
{
    public class CharacterEquipment : MonoBehaviour
    {
        [Header("Weapon")]
        public Weapon primaryWeapon;
        public Weapon secondaryWeapon;
        [Header("Items")]
        /*public Armor currentArmor;
        public Boots currentBoots;
        public Necklace currentNecklace;
        public Ring currentRing;*/
        [SerializeField]
        private Controller controller;
        private CharacterAnimator characterAnimator;

        public void Initialize(Controller _controller, CharacterAnimator _characterAnimator)
        {
            this.controller = _controller;
            this.characterAnimator = _characterAnimator;
            if (primaryWeapon != null)
            {
                primaryWeapon.Initialize(controller);
                primaryWeapon.isPrimaryWeapon = true;
            }
            if (secondaryWeapon != null)
            {
                secondaryWeapon.Initialize(controller);
                secondaryWeapon.gameObject.SetActive(false);
                primaryWeapon.isPrimaryWeapon = false;
            }
        }

        public void HandlePrimaryAttack()
        {
            if (primaryWeapon == null) return;
            if (secondaryWeapon != null)
            {
                secondaryWeapon.OnUnEquip();
                secondaryWeapon.gameObject.SetActive(false);
            }
            primaryWeapon.gameObject.SetActive(true);
            if (!primaryWeapon.equipWeapon)
                primaryWeapon.OnEquip();
            primaryWeapon.TriggerWeapon();
        }

        public void HandleSecondaryAttack()
        {
            if (secondaryWeapon == null) return;
            if (primaryWeapon != null)
            {
                primaryWeapon.OnUnEquip();
                primaryWeapon.gameObject.SetActive(false);
            }
            secondaryWeapon.gameObject.SetActive(true);
            if (!secondaryWeapon.equipWeapon)
                secondaryWeapon.OnEquip();
            secondaryWeapon.TriggerWeapon();
        }

        public void UpdateLogic()
        {
            if (primaryWeapon != null)
                primaryWeapon.OnUpdate();
            if (secondaryWeapon != null)
                secondaryWeapon.OnUpdate();
        }
    }
}
