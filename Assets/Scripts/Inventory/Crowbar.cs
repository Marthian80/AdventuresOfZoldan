using System.Collections;
using UnityEngine;

public class Crowbar : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    
    [SerializeField] private float attackCoolDown = 1.25f;
        
    private Animator animator;    
    private GameObject slashAnimation;
    private Transform weaponCollider;

    private void Awake()
    {        
        animator = GetComponent<Animator>();        
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = PlayerController.Instance.GetSlashAnimationSpawnPoint();
    }

    private void Update()
    {
        MouseFollowWithOffset();     
    }

    public void Attack()
    {   
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slashAnimation = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;

        StartCoroutine(AttackCDRoutine());                
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(attackCoolDown);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }

    public void AttackFinishedAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerPos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else if (mousePos.x > playerPos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
