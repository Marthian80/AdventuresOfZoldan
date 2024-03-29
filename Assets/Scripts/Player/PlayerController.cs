using AdventureOfZoldan.Core;
using AdventureOfZoldan.Core.Saving;
using AdventureOfZoldan.Misc;
using AdventureOfZoldan.SceneManagement;
using System;
using UnityEngine;

namespace AdventureOfZoldan.Player
{
    public class PlayerController : Singleton<PlayerController>, ISaveable
    {
        public bool FacingLeft { get { return facingLeft; } }

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private Transform weaponCollider;
        [SerializeField] private Transform slashAnimSpawnPoint;
        
        private enum CursorType
        {
            None,
            Combat
        }

        private PlayerControls playerControls;
        private Vector2 movement;
        private Rigidbody2D rb;
        private Animator playerAnimator;
        private SpriteRenderer spriteRenderer;
        private Knockback knockback;
        private Flash flash;
        private Vector3 startPosition;

        private bool facingLeft = false;

        protected override void Awake()
        {
            base.Awake();

            playerControls = new PlayerControls();
            rb = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            knockback = GetComponent<Knockback>();
            flash = GetComponent<Flash>();
            
            startPosition = gameObject.transform.position;                     
        }

        private void Start()
        {
            SceneManager.Instance.onGameRestarted += RestartPlayer;
        }

        private void RestartPlayer()
        {
            playerControls.Disable();
            gameObject.transform.position = startPosition;
            playerControls.Enable();

            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1);
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void Update()
        {
            PlayerInput();
            AdjustPlayerFacingDirection();
        }

        private void FixedUpdate()
        {
            if (!SceneManager.Instance.GamePaused)
            {
                Move();
            }                
        }

        public Transform GetWeaponCollider()
        {
            return weaponCollider;
        }

        public Transform GetSlashAnimationSpawnPoint()
        {
            return slashAnimSpawnPoint;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            transform.position = position.ToVector();
            Debug.Log("Restore called");
        }

        private void PlayerInput()
        {
            movement = playerControls.Movement.Move.ReadValue<Vector2>();

            playerAnimator.SetFloat("moveX", movement.x);
            playerAnimator.SetFloat("moveY", movement.y);
        }

        private void Move()
        {
            if (knockback.KnockedBackActive) { return; }
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        }

        private void AdjustPlayerFacingDirection()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

            if (mousePos.x < playerPos.x)
            {
                spriteRenderer.flipX = true;
                facingLeft = true;
            }
            else if (mousePos.x > playerPos.x)
            {
                spriteRenderer.flipX = false;
                facingLeft = false;
            }
        }
    }
}