using AdventureOfZoldan.Core;
using AdventureOfZoldan.Core.Saving;
using AdventureOfZoldan.Inventories;
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
                    DisableSpriteWhenDestroyed(true);
                }
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
                GetComponent<LootSpawner>()?.DropLoot();
                isDestroyed = true;
            }
        }

        private void DisableSpriteWhenDestroyed(bool destroyed)
        {
            var sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                sprite.enabled = !destroyed;
            }
            GetComponent<CapsuleCollider2D>().enabled = !destroyed;
        }

        private IEnumerator DestroyAnimationRoutine()
        {
            theAnimator.SetTrigger(EXPLODE_HASH);
            AudioPlayer.Instance.PlayExplosionOneClip(transform.position);
            yield return new WaitForSeconds(0.75f);
            DisableSpriteWhenDestroyed(true);            
        }

        public object CaptureState()
        {
            return isDestroyed;
        }

        public void RestoreState(object state)
        {            
            DisableSpriteWhenDestroyed((bool)state);                        
        }
    }
}