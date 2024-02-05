using AdventureOfZoldan.Enemies;
using AdventureOfZoldan.Player;
using AdventureOfZoldan.Weapons;
using UnityEngine;

namespace AdventureOfZoldan.Misc
{
    public class DamageSource : MonoBehaviour
    {
        private int damageAmount;

        private void Start()
        {
            MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
            damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);
        }
    }
}