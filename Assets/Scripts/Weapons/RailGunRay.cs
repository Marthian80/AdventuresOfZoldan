using AdventureOfZoldan.Misc;
using System.Collections;
using UnityEngine;

namespace AdventureOfZoldan.Weapons
{
    public class RailGunRay : MonoBehaviour
    {
        [SerializeField] private float rayGrowTime = 2f;

        private bool isGrowing = true;
        private float rayRange;
        private SpriteRenderer spriteRenderer;
        private CapsuleCollider2D capsuleCollider;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        private void Start()
        {
            RayFaceMouse();
        }

        public void UpdateRayRange(float rayRange)
        {
            this.rayRange = rayRange;
            StartCoroutine(IncreaseRayLengthRoutine());
        }

        private IEnumerator IncreaseRayLengthRoutine()
        {
            float timePassed = 0f;

            while (spriteRenderer.size.x < rayRange && isGrowing)
            {
                timePassed += Time.deltaTime;
                float linearT = timePassed / rayGrowTime;

                spriteRenderer.size = new Vector2(Mathf.Lerp(1f, rayRange, linearT), 1f);
                capsuleCollider.size = new Vector2(Mathf.Lerp(1f, rayRange, linearT), capsuleCollider.size.y);
                capsuleCollider.offset = new Vector2((Mathf.Lerp(1f, rayRange, linearT) / 2), capsuleCollider.offset.y);

                yield return null;
            }

            StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Indestructable indestructable = collision.gameObject.GetComponent<Indestructable>();

            if (!collision.isTrigger && indestructable)
            {
                isGrowing = false;
            }
        }

        private void RayFaceMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = transform.position - mousePosition;

            transform.right = -direction;
        }
    }
}