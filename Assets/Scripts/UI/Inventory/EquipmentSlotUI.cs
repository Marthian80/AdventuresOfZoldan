using AdventureOfZoldan.Inventories;
using AdventureOfZoldan.UI.Inventories.Dragging;
using System;
using UnityEngine;

namespace AdventureOfZoldan.UI.Inventories
{
    public class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] EquipLocation equipLocation = EquipLocation.Weapon;

        private ActiveInventory activeInventory;

        //STATE
        EquipableItem item = null;

        private void Start()
        {
            activeInventory = gameObject.GetComponentInParent<ActiveInventory>();
        }

        public void AddItems(InventoryItem item, int number)
        {
            this.item = item as EquipableItem;
            icon.SetItem(item, number);
            activeInventory.ItemEquiped();
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public int GetNumber()
        {
            if (item == null)
            {
                return 0;
            }
            return 1;
        }

        public int MaxAcceptable(InventoryItem item)
        {            
            EquipableItem equipableItem = item as EquipableItem;
            if (equipableItem == null) return 0;
            if (equipableItem.GetAllowedEquipLocation() != equipLocation) { return 0; }
            if (this.item != null) { return 0; }

            return 1;
        }

        public void RemoveItems(int number)
        {
            this.item = null;
            icon.SetItem(null, 0);
            activeInventory.ItemEquiped();
        }
    }
}

