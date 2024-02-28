using AdventureOfZoldan.Core.Saving;
using AdventureOfZoldan.Inventories;
using AdventureOfZoldan.Misc;
using AdventureOfZoldan.Player;
using System.Collections;
using UnityEngine;

namespace AdventureOfZoldan.Enemies
{
    public class EnemyHealth : MonoBehaviour, ISaveable
    {
        [SerializeField] private int startingHealth = 3;
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private float knockBackThrust = 9f;

        private int currentHealth;
        private Knockback knockback;
        private Flash flash;

        private void Awake()
        {
            knockback = GetComponent<Knockback>();
            flash = GetComponent<Flash>();
        }

        private void Start()
        {
            currentHealth = startingHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
            StartCoroutine(flash.FlashRoutine());
            StartCoroutine(CheckDetectDeathRoutine());
        }

        public object CaptureState()
        {
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            currentHealth = (int)state;

            if (currentHealth <= 0)
            {
                DetectDeath();
            }
            else
            {
                RestoreToLife();
            }            
        }

        private IEnumerator CheckDetectDeathRoutine()
        {
            yield return new WaitForSeconds(flash.GetFlashTime());
            DetectDeath();
        }

        private void DetectDeath()
        {
            if (currentHealth <= 0)
            {
                Instantiate(deathVFX, transform.position, Quaternion.identity);
                var sprites = GetComponentsInChildren<SpriteRenderer>();
                foreach (var sprite in sprites)
                {
                    sprite.enabled = false;
                }
                GetComponent<EnemyPathfinding>().StopMoving();
                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponent<LootSpawner>()?.DropLoot();
            }
        }

        private void RestoreToLife()
        {
            var sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                sprite.enabled = true;
            }
            GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}