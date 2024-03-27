using AdventureOfZoldan.Inventories;
using AdventureOfZoldan.Weapons;
using UnityEngine;

namespace AdventureOfZoldan.UI.Inventories
{
    /// <summary>
    /// An inventory item that can be equipped to the player. Weapons could be a
    /// subclass of this.
    /// </summary>
    [CreateAssetMenu(menuName = ("AdventureOfZoldan/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        // CONFIG DATA
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

        [Tooltip("IF this can be used as a weapon, the associating weapon info SO to be put here")]
        [SerializeField] WeaponInfo weaponInfo;

        // PUBLIC

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }

        public WeaponInfo GetWeaponInfo()
        {
            return weaponInfo;
        }
    }
}