using UnityEngine;

public class LaserPistol : MonoBehaviour, IWeapon
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AdjustGunFacingDirection();
    }

    public void Attack()
    {
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }

    private void AdjustGunFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerPos.x)
        {
            spriteRenderer.flipY = true;            
        }
        else if (mousePos.x > playerPos.x)
        {
            spriteRenderer.flipY = false;            
        }
    }
}