using AdventureOfZoldan.Player;
using AdventureOfZoldan.Core;
using UnityEngine;

namespace AdventureOfZoldan.Weapons
{
    public class LaserPistol : MonoBehaviour, IWeapon
    {
        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private GameObject laserBlastPrefab;
        [SerializeField] private Transform laserBlastSpawnPoint;

        private SpriteRenderer spriteRenderer;

        private Animator theAnimator;
        private float spawnpointOffset = 0.1f;
        private readonly int FIRE_HASH = Animator.StringToHash("Fire");

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            theAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            AdjustGunFacingDirection();
        }

        public void Attack()
        {
            theAnimator.SetTrigger(FIRE_HASH);

            GameObject newLaserBlast = Instantiate(laserBlastPrefab,
                new Vector3(laserBlastSpawnPoint.position.x + spawnpointOffset, laserBlastSpawnPoint.position.y + spawnpointOffset, laserBlastSpawnPoint.position.z),
                ActiveWeapon.Instance.transform.rotation);
            newLaserBlast.GetComponent<Projectile>().UpdateWeaonInfo(weaponInfo);
            AudioPlayer.Instance.PlayLaserPistolClip();
        }

        public WeaponInfo GetWeaponInfo()
        {
            return weaponInfo;
        }

        private void AdjustGunFacingDirection()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 playerPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

            if (mousePos.x < playerPos.x)
            {
                spriteRenderer.flipY = true;
                foreach (var spriteRenderChild in GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderChild.flipY = true;
                }
            }
            else if (mousePos.x > playerPos.x)
            {
                spriteRenderer.flipY = false;
                foreach (var spriteRenderChild in GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderChild.flipY = false;
                }
            }
        }
    }
}
