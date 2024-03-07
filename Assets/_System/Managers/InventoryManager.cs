using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Utils.String;

namespace MyHeroWay
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        public List<InventoryStackSlot> materials;
        public SerializedDictionary<ItemDataSO, InventorySlot> materialsDictionary;
        public List<InventorySlot> equipments;
        public SerializedDictionary<ItemDataSO, InventorySlot> equipmentsDictionary;
        public Action UpdateItemUI;

        protected override void Awake()
        {
            base.Awake();
            materials = new List<InventoryStackSlot>();
            equipments = new List<InventorySlot>();
            materialsDictionary = new SerializedDictionary<ItemDataSO, InventorySlot>();
            equipmentsDictionary = new SerializedDictionary<ItemDataSO, InventorySlot>();
        }
        public void AddItem(ItemGame item)
        {
            if (item.itemType is EItemType.Equipment)
            {

            }
            if (item.itemType is EItemType.Material)
            {
                AddMaterial(item);
                MaterialData material = item.data as MaterialData;
                DataManager.Instance.AddMaterial(material);
            }
            UpdateItem();
        }

        public void RemoveItem(ItemGame item)
        {
            if (item.itemType is EItemType.Equipment)
            {

            }
            if (item.itemType is EItemType.Material)
            {
                RemoveMaterial(item);
            }
            UpdateItem();
        }

        public void UpdateItem()
        {
            foreach (var item in materialsDictionary)
            {
                if (materials.Contains(item.Value)) continue;
                materials.Add(item.Value as InventoryStackSlot);
            }
            UpdateItemUI?.Invoke();
        }

        private void AddMaterial(ItemGame item)
        {
            if (materialsDictionary.TryGetValue(item.dataSO, out InventorySlot material))
            {
                var stack = material as InventoryStackSlot;
                stack.AddStack();

            }
            else
            {
                InventoryStackSlot newItem = new InventoryStackSlot(item.dataSO);
                materials.Add(newItem);
                materialsDictionary.Add(item.dataSO, newItem);

            }
        }

        private void RemoveMaterial(ItemGame item)
        {
            if (materialsDictionary.TryGetValue(item.dataSO, out InventorySlot material))
            {
                var stack = material as InventoryStackSlot;
                if (stack.stackSize <= 1)
                {
                    stack.RemoveStack();
                    materialsDictionary.Remove(item.dataSO);
                }
                else
                {
                    stack.RemoveStack();
                }
            }
        }

        public void AddEquipment(ItemGame item)
        {
            if (materialsDictionary.TryGetValue(item.dataSO, out InventorySlot equipment))
            {

            }
            else
            {
                /*InventoryStackSlot newItem = new InventoryStackSlot();
                materials.Add(newItem);
                materialsDictionary.Add(item, newItem);*/

            }
        }

        public void SpawnMaterialUI(Transform holder, List<InventoryStackSlotUI> owner)
        {
            foreach (var item in materials)
            {
                AddInventoryStackUI(holder, owner);
            }
        }

        public void SpawnEquipmentUI(Transform holder, List<InventorySlotUI> owner)
        {
            foreach (var item in equipments)
            {
                AddInventoryUI(holder, owner);
            }
        }

        public void AddInventoryStackUI(Transform holder, List<InventoryStackSlotUI> owner)
        {
            owner.Add(FactoryObject.Spawn<InventoryStackSlotUI>(StrManager.UIPool, StrManager.InventoryStackUI_Prefab, holder));
        }

        public void AddInventoryUI(Transform holder, List<InventorySlotUI> owner)
        {
            owner.Add(FactoryObject.Spawn<InventorySlotUI>(StrManager.UIPool, StrManager.InventoryUI_Prefab, holder));
        }

        public void RemoveInventoryUI(InventorySlotUI inventory, List<InventorySlotUI> owner)
        {
            owner.Remove(inventory);
            FactoryObject.Despawn(StrManager.UIPool, inventory.transform, PoolManager.Pools[StrManager.UIPool].transform);
        }
        public void RemoveInventoryStackUI(InventoryStackSlotUI inventory, List<InventoryStackSlotUI> owner)
        {
            owner.Remove(inventory);
            FactoryObject.Despawn(StrManager.UIPool, inventory.transform, PoolManager.Pools[StrManager.UIPool].transform);
        }

       
    }
}
