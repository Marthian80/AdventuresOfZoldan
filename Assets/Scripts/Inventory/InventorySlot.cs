using AdventureOfZoldan.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureOfZoldan.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private WeaponInfo weaponInfo;

        public WeaponInfo GetWeaponInfo() { return weaponInfo; }
    }
}