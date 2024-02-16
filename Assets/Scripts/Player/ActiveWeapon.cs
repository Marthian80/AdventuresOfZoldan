using AdventureOfZoldan.Core;
using AdventureOfZoldan.SceneManagement;
using AdventureOfZoldan.Weapons;
using System.Collections;
using UnityEngine;

namespace AdventureOfZoldan.Player
{
    public class ActiveWeapon : Singleton<ActiveWeapon>
    {
        public MonoBehaviour CurrentActiveWeapon { get; private set; }

        private PlayerControls playerControls;
        private bool attackButtonDown, isAttacking = false;
        private float timeBetweenAttacks;

        protected override void Awake()
        {
            base.Awake();
            playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void Start()
        {
            playerControls.Combat.Attack.started += _ => StartAttacking();
            playerControls.Combat.Attack.canceled += _ => StopAttacking();

            AttackCoolDown();
        }

        private void Update()
        {
            if (!SceneManager.Instance.GamePaused)
            {
                Attack();
            }                
        }

        public void SelectNewWeapon(MonoBehaviour weapon)
        {
            CurrentActiveWeapon = weapon;
            AttackCoolDown();
            timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCoolDown;
        }


        public void NoWeaponSelected()
        {
            CurrentActiveWeapon = null;
        }

        private void AttackCoolDown()
        {
            isAttacking = true;
            StopAllCoroutines();
            StartCoroutine(TimeBetweenAttackingRoutine());
        }

        private IEnumerator TimeBetweenAttackingRoutine()
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            isAttacking = false;
        }

        private void StartAttacking()
        {
            attackButtonDown = true;
        }

        private void StopAttacking()
        {
            attackButtonDown = false;
        }

        private void Attack()
        {
            if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
            {
                AttackCoolDown();
                (CurrentActiveWeapon as IWeapon).Attack();
            }
        }
    }
}