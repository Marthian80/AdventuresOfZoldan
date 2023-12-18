using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject railGunRayPrefab;
    [SerializeField] private Transform railGunRaySpawnPoint;
    
    private SpriteRenderer spriteRenderer;
    private Animator theAnimator;
    private float spawnpointOffset = 0.12f;

    private readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        theAnimator= GetComponent<Animator>();
    }

    private void Update()
    {
        AdjustGunFacingDirection();
    }

    public void Attack()
    {
        theAnimator.SetTrigger(FIRE_HASH);        
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void SpawnRailGunProjectileAnimEvent()
    {
        GameObject newRay = Instantiate(railGunRayPrefab,
            new Vector3(railGunRaySpawnPoint.position.x + spawnpointOffset, railGunRaySpawnPoint.position.y, railGunRaySpawnPoint.position.z),
            ActiveWeapon.Instance.transform.rotation);
        newRay.GetComponent<RailGunRay>().UpdateRayRange(weaponInfo.weaponRange);
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
