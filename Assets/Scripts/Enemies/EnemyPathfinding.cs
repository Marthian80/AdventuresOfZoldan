using AdventureOfZoldan.Misc;
using UnityEngine;

namespace AdventureOfZoldan.Enemies
{
    public class EnemyPathfinding : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        private Rigidbody2D rb;
        private Vector2 moveDirection;
        private Knockback knockback;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            knockback = GetComponent<Knockback>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (knockback.KnockedBackActive)
            {
                return;
            }
            Move();
        }

        private void Move()
        {
            rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));

            if (moveDirection.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        public void MoveTo(Vector2 pos)
        {
            moveDirection = pos;
        }
    }
}