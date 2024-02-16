using AdventureOfZoldan.Core.Saving;
using AdventureOfZoldan.Enemies;
using AdventureOfZoldan.Misc;
using AdventureOfZoldan.SceneManagement;
using System;
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
        [SerializeField] private float deathDelay = 1.25f;

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
            SceneManager.Instance.onGameRestarted += SetBackToStartingHealth;

            SetBackToStartingHealth();
        }                

        public void HealPlayer(int healingAmount)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += healingAmount;
                UpdateHealthSlider();
            }
        }

        private void SetBackToStartingHealth()
        {
            currentHealth = maxHealth;
            UpdateHealthSlider();
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
            
            StartCoroutine(DamageRecoveryRoutine());
            UpdateHealthSlider();
            CheckIfPlayerDeath();
        }

        private void CheckIfPlayerDeath()
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                StartCoroutine(flash.FadeOutRoutine(spriteRenderer, deathDelay));
                StartCoroutine(DeathLoadSceneRoutine());
            }
        }

        private IEnumerator DeathLoadSceneRoutine()
        {
            yield return new WaitForSeconds(deathDelay);
            SceneManager.Instance.ShowGameOverScreen();
            SceneManager.Instance.PauseGame();
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