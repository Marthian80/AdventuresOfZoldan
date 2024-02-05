using AdventureOfZoldan.Core.Saving;
using AdventureOfZoldan.Enemies;
using AdventureOfZoldan.Misc;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureOfZoldan.Player
{
    public class PlayerHealth : MonoBehaviour, ISaveable
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private float knockbackThrustAmount = 10f;
        [SerializeField] private float damageRecoveryTime = 1;

        private Slider healthSlider;
        private int currentHealth;
        private bool canTakeDamage = true;

        private Knockback knockback;
        private Flash flash;

        private void Awake()
        {
            knockback = GetComponent<Knockback>();
            flash = GetComponent<Flash>();
        }

        private void Start()
        {
            currentHealth = maxHealth;

            UpdateHealthSlider();
        }

        public void HealPlayer(int healingAmount)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += healingAmount;
                UpdateHealthSlider();
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemy && canTakeDamage)
            {
                knockback.GetKnockedBack(collision.gameObject.transform, knockbackThrustAmount);
                StartCoroutine(flash.FlashRoutine());
                TakeDamage(1);
            }
        }

        private void TakeDamage(int damageAmount)
        {
            canTakeDamage = false;
            currentHealth -= damageAmount;
            Debug.Log(currentHealth.ToString());
            StartCoroutine(DamageRecoveryRoutine());
            UpdateHealthSlider();
            CheckIfPlayerDeath();
        }

        private void CheckIfPlayerDeath()
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("player died");
            }
        }

        private IEnumerator DamageRecoveryRoutine()
        {
            yield return new WaitForSeconds(damageRecoveryTime);
            canTakeDamage = true;
        }

        private void UpdateHealthSlider()
        {
            if (healthSlider == null)
            {
                healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
            }

            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        public object CaptureState()
        {
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            currentHealth = (int)state;
            CheckIfPlayerDeath();
            UpdateHealthSlider();
        }
    }
}