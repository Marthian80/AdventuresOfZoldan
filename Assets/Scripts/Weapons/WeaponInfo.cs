using UnityEngine;

namespace AdventureOfZoldan.Weapons
{
    [CreateAssetMenu(menuName = "New Weapon")]
    public class WeaponInfo : ScriptableObject
    {
        public GameObject weaponPrefab;
        public float weaponCoolDown;
        public int weaponDamage;
        public float weaponRange;
    }
}