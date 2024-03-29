using AdventureOfZoldan.Inventories;
using AdventureOfZoldan.UI.Inventories.Dragging;
using System;
using UnityEngine;

namespace AdventureOfZoldan.UI.Inventories
{
    public class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] EquipLocation equipLocation = EquipLocation.None;
        [SerializeField] int slotNumber = 0;        

        private ActiveInventory activeInventory;
        private Equipment playerEquipment;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerEquipment = player.GetComponent<Equipment>();
            playerEquipment.equipmentUpdated += RedrawUI;
        }        

        private void Start()
        {
            activeInventory = gameObject.GetComponentInParent<ActiveInventory>();
            RedrawUI();
        }

        public void AddItems(InventoryItem item, int number)
        {
            playerEquipment.AddItem(equipLocation, (EquipableItem)item, slotNumber);
            activeInventory.ItemEquiped();
        }

        public InventoryItem GetItem()
        {
            return playerEquipment.GetItemInSlot(equipLocation, slotNumber); 
        }

        public int GetNumber()
        {
            if (GetItem() != null)
            {
                return 1;
            }
            return 0;
        }

        public int MaxAcceptable(InventoryItem item)
        {            
            EquipableItem equipableItem = item as EquipableItem;
            if (equipableItem == null) return 0;
            if (equipableItem.GetAllowedEquipLocation() != equipLocation) { return 0; }
            if (GetItem() != null) { return 0; }

            return 1;
        }

        public void RemoveItems(int number)
        {
            playerEquipment.RemoveItem(equipLocation, slotNumber);
            activeInventory.ItemEquiped();
        }

        private void RedrawUI()
        {
            var equipmentItem = playerEquipment.GetItemInSlot(equipLocation, slotNumber);
            icon.SetItem(equipmentItem, equipmentItem == null ? 0 : 1);
        }
    }
}

