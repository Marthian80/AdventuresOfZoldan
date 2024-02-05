using AdventureOfZoldan.Core.Saving;
using AdventureOfZoldan.Weapons;
using System.Collections;
using UnityEngine;

namespace AdventureOfZoldan.Misc
{
    public class Destructable : MonoBehaviour, ISaveable
    {
        [SerializeField] private GameObject destroyVFX;

        private bool isDestroyed = false;
        private Animator theAnimator;

        private readonly int EXPLODE_HASH = Animator.StringToHash("Explode");

        private void Awake()
        {
            theAnimator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<DamageSource>() || collision.gameObject.GetComponent<Projectile>())
            {
                if (theAnimator != null)
                {
                    StartCoroutine(DestroyAnimationRoutine());
                }
                else
                {
                    DisableSpriteWhenDestroyed();
                }
                Instantiate(destroyVFX, transform.position, Quaternion.identity);

                isDestroyed = true;
            }
        }

        private void DisableSpriteWhenDestroyed()
        {
            var sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                sprite.enabled = false;
            }
            GetComponent<CapsuleCollider2D>().enabled = false;
        }

        private IEnumerator DestroyAnimationRoutine()
        {
            theAnimator.SetTrigger(EXPLODE_HASH);
            yield return new WaitForSeconds(0.75f);
            DisableSpriteWhenDestroyed();
        }

        public object CaptureState()
        {
            return isDestroyed;
        }

        public void RestoreState(object state)
        {
            isDestroyed = (bool)state;
            if (isDestroyed)
            {
                DisableSpriteWhenDestroyed();
            }
        }
    }
}