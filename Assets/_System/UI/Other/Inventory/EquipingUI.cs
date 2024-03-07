using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class EquipingUI : MonoBehaviour
    {
        public List<Image> plusIcon;
        public Transform PrimarySlot;
        public Transform SecondarySlot;
        public Transform HelmetSlot;
        public Transform ArmorSlot;
        public Transform GauntletSlot;
        public Transform RingSlot;
        public Transform AmuletSlot;
        public Transform BootsSlot;
        public void Initialize()
        {
            foreach (var item in plusIcon)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(item.materialForRendering.DOFade(0.5f, 1f));
                sequence.Append(item.materialForRendering.DOFade(1f, 1f));
                sequence.SetLoops(-1, LoopType.Yoyo);
            }
        }

        [Button("GetReference")]
        private void GetReference()
        {
            SecondarySlot = transform.GetChild(0).Find("Secondary Weapon").GetChild(3);
            ArmorSlot = transform.GetChild(0).Find("Armor").GetChild(3);
            AmuletSlot = transform.GetChild(0).Find("Amulet").GetChild(3);
            BootsSlot = transform.GetChild(0).Find("Boot").GetChild(3);
            PrimarySlot = transform.GetChild(1).Find("Primary Weapon").GetChild(3);
            HelmetSlot = transform.GetChild(1).Find("Helmet").GetChild(3);
            GauntletSlot = transform.GetChild(1).Find("Gauntlet").GetChild(3);
            RingSlot = transform.GetChild(1).Find("Ring").GetChild(3);
        }
    }
}
