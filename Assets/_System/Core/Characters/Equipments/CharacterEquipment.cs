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
            primaryWeapon.Initialize(controller);
        }
    }
}
