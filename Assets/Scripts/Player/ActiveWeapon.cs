using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private PlayerControls playerControls;
    private bool attackButtonDown, isAttacking = false;

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
    }

    private void Update()
    {
        Attack();
    }

    public void SelectNewWeapon(MonoBehaviour weapon)
    {
        CurrentActiveWeapon = weapon;
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    public void NoWeaponSelected()
    {
        CurrentActiveWeapon = null;
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
        if (attackButtonDown && !isAttacking) 
        {
            isAttacking = true;

            (CurrentActiveWeapon as IWeapon).Attack();
        }        
    }
}
