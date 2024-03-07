using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class MaterialGame : ItemGame
    {
        public bool isDrop;
        public SpriteRenderer visual;
        public SpriteRenderer shadow;
        public void AddMaterial()
        {
            InventoryManager.Instance.AddItem(this);
        }

        private void OnValidate()
        {
            Initialize(dataSO);
        }

        public void Initialize(ItemDataSO dataSO)
        {
            this.dataSO = dataSO;
            this.data = new ItemData();
            this.data.inventoryID = DataManager.Instance.data.inventoryData.idItemDefine;
            this.data.itemID = dataSO.id;
            visual.sprite = dataSO.icon;
            shadow.sprite = dataSO.icon;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(StrManager.PlayerTag))
            {
                if (!isDrop)
                {

                }
                else
                {
                    AddMaterial();
                }
            }
        }
    }
}
