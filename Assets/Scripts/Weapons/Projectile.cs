using AdventureOfZoldan.Enemies;
using AdventureOfZoldan.Misc;
using UnityEngine;

namespace AdventureOfZoldan.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 23f;
        [SerializeField] private GameObject particlePrefabonHitVFX;

        private WeaponInfo weaponInfo;
        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            MoveProjectile();
            DetectFireDistance();
        }

        public void UpdateWeaonInfo(WeaponInfo weaponInfo)
        {
            this.weaponInfo = weaponInfo;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            Indestructable indestructable = collision.gameObject.GetComponent<Indestructable>();

            if (!collision.isTrigger && (enemyHealth || indestructable))
            {
                Instantiate(particlePrefabonHitVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        private void DetectFireDistance()
        {
            if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
            {
                Destroy(gameObject);
            }
        }


        private void MoveProjectile()
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
    }
}