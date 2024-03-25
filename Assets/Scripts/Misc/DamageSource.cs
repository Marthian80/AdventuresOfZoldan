using AdventureOfZoldan.Core;
using AdventureOfZoldan.Enemies;
using AdventureOfZoldan.Player;
using AdventureOfZoldan.Weapons;
using UnityEngine;

namespace AdventureOfZoldan.Misc
{
    public class DamageSource : MonoBehaviour
    {
        private int damageAmount;
        private bool isHandWeaponDamage;

        private void Start()
        {
            MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
            damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
            isHandWeaponDamage = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponRange < 1;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);
            if (enemyHealth && isHandWeaponDamage)
            {
                AudioPlayer.Instance.PlayHandWeaponImpactClip(collision.gameObject.transform.position);
            }
        }
    }
}