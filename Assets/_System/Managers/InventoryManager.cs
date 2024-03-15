using Cysharp.Threading.Tasks;
using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Utils.String;
using Utils;

namespace MyHeroWay
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        public List<InventoryStackSlot> materials;
        public SerializedDictionary<ItemDataSO, InventorySlot> materialsDictionary;
        public List<InventorySlot> equipments;
        public SerializedDictionary<ItemDataSO, InventoryColection> equipmentsDictionary;
        public Action UpdateItemUI;
        private UserData userData;

        protected override void Awake()
        {
            base.Awake();
            materials = new List<InventoryStackSlot>();
            equipments = new List<InventorySlot>();
            materialsDictionary = new SerializedDictionary<ItemDataSO, InventorySlot>();
            equipmentsDictionary = new SerializedDictionary<ItemDataSO, InventoryColection>();
        }

        public async void Initialize(UserData userData)
        {
            this.userData = userData;
            List<UniTask> tasks = new List<UniTask>()
            {
                 LoadMaterialsData(),
                 LoadEquipmentsData()
            };
            await UniTask.WhenAll(tasks);
            UpdateItemUI?.Invoke();
        }

        private async UniTask LoadMaterialsData()
        {
            var itemDataSO = DataManager.Instance.itemContainer;
            var materialOwned = userData.inventoryData.materialOwned;
            if (materialOwned.Count == 0)
                return;


            foreach (var item in materialOwned)
            {
                var key = await Delay.DoAction(() => itemDataSO.GetItemObject(item.itemID));
                if (key != null)
                {
                    var value = new InventoryStackSlot(key);
                    value.ItemData = item;
                    value.stackSize = item.stackSize;
                    materialsDictionary.Add(key, value);
                    materials.Add(value);
                }

            }
        }

        private async UniTask LoadEquipmentsData()
        {
            var equipmentDataSO = DataManager.Instance.equipmentContainer;
            var equipmentsOwned = userData.inventoryData.equipmentsOwned;
            if (equipmentsOwned.Count == 0)
                return;

            foreach (var item in equipmentsOwned)
            {
                var key = await Delay.DoAction(() => equipmentDataSO.GetEquipmentObject(item.itemID));
                if (key != null)
                {
                    var data = new InventorySlot(key);
                    data.ItemData = item;
                    if (equipmentsDictionary.TryGetValue(key, out InventoryColection value))
                    {
                        value.InventorySlots.Add(data);
                    }
                    else
                    {
                        var collection = new InventoryColection();
                        collection.InventorySlots.Add(data);
                        equipmentsDictionary.Add(key, collection);
                    }
                    equipments.Add(data);
                }

            }
        }


        public void AddItem(ItemGame item)
        {
            if (item.itemType is EItemType.Equipment)
            {
                EquipmentData equipment = new EquipmentData(item.dataSO.id);
                item.data = DataManager.Instance.AddEquipment(equipment);
                AddEquipment(item);


            }
            if (item.itemType is EItemType.Material)
            {
                AddMaterial(item);
                MaterialData material = new MaterialData(item.dataSO.id);
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
            InventorySlot inventorySlot = new InventorySlot(item);
            equipments.Add(inventorySlot);
            if (equipmentsDictionary.TryGetValue(item.dataSO, out InventoryColection listEquipments))
            {
                listEquipments.InventorySlots.Add(inventorySlot);
            }
            else
            {
                InventoryColection colection = new InventoryColection();
                colection.InventorySlots.Add(inventorySlot);
                equipmentsDictionary.Add(item.dataSO, colection);
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
