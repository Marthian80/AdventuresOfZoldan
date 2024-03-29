using AdventureOfZoldan.Core.Saving;
using AdventureOfZoldan.UI.Inventories;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventureOfZoldan.Inventories
{
    public class Equipment : MonoBehaviour, ISaveable
    {
        // STATE
        private List<EquipLocationSlot> equippedItems = new List<EquipLocationSlot>();

        private struct EquipLocationSlot
        {
            public EquipableItem item;
            public EquipLocation location;
            public int slotNumber;
        }

        /// <summary>
        /// Broadcasts when the items in the slots are added/removed.
        /// </summary>
        public event Action equipmentUpdated;

        /// <summary>
        /// Return the item in the given equip location.
        /// </summary>
        public EquipableItem GetItemInSlot(EquipLocation equipLocation, int slotNumber)
        {
            var item = equippedItems.Where(x => x.location == equipLocation && x.slotNumber == slotNumber).ToArray();
            if (item.Length == 0)
            {
                return null;
            }

            return item[0].item;
        }

        /// <summary>
        /// Add an item to the given equip location. Do not attempt to equip to
        /// an incompatible slot.
        /// </summary>
        public void AddItem(EquipLocation slot, EquipableItem item, int slotNumber)
        {
            Debug.Assert(item.GetAllowedEquipLocation() == slot);

            equippedItems.Add(new EquipLocationSlot
            {
                item = item,
                location = slot,
                slotNumber = slotNumber
            });

            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        /// <summary>
        /// Remove the item for the given slot.
        /// </summary>
        public void RemoveItem(EquipLocation slot, int slotNumber)
        {
            var index = equippedItems.FindIndex(x => x.location == slot && x.slotNumber == slotNumber);
            equippedItems.RemoveAt(index);
            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        [System.Serializable]
        private struct EquipmentSlotRecord
        {
            public string itemID;
            public int number;
            public EquipLocation location;
        }

        public object CaptureState()
        {
            var numberOfRecrods = equippedItems.Count;
            var equipmentRecords = new EquipmentSlotRecord[numberOfRecrods];
            for (int i = 0; i < numberOfRecrods; i++)
            {
                if (equippedItems[i].item != null)
                {
                    equipmentRecords[i].itemID = equippedItems[i].item.GetItemID();
                    equipmentRecords[i].number = equippedItems[i].slotNumber;
                    equipmentRecords[i].location = equippedItems[i].location;
                }
            }
            return equipmentRecords;            
        }

        public void RestoreState(object state)
        {            
            var slotRecords = (EquipmentSlotRecord[])state;
            var numberOfRecords = slotRecords.Length;
            equippedItems = new List<EquipLocationSlot>();
            foreach(var equipment in slotRecords)
            {
                equippedItems.Add(new EquipLocationSlot
                {
                    item = InventoryItem.GetFromID(equipment.itemID) as EquipableItem,
                    slotNumber = equipment.number,
                    location = equipment.location
                });
            }
            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }
    }
}

