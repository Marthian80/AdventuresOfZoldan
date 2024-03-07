using AdventureOfZoldan.Misc;
using AdventureOfZoldan.Player;
using AdventureOfZoldan.SceneManagement;
using AdventureOfZoldan.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureOfZoldan.Inventories
{
    public class ActiveInventory : MonoBehaviour
    {
        private int activeSlotIndexNum = 0;

        private PlayerControls playerControls;
        private Flash flash;
        private PlayerHealth playerHealth;
        private GameObject newWeapon;

        private void Awake()
        {
            playerControls = new PlayerControls();
            var player = GameObject.FindWithTag("Player");
            flash = player.GetComponent<Flash>();
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        private void Start()
        {
            playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
            playerHealth.onPlayerDead += FadeoutWeapon;
            ToggleActiveSlot(1);

            SceneManager.Instance.onGameRestarted += RestartPlayer;            
        }

        private void RestartPlayer()
        {
            foreach (var spriteRenderChild in newWeapon.GetComponentsInChildren<SpriteRenderer>())
            {
                var color = spriteRenderChild.color;
                spriteRenderChild.color = new Color(color.r, color.g, color.b, 1);                
            }                    
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void FadeoutWeapon(float fadeOutDelay)
        {
            var spriteRenderer = newWeapon.GetComponent<SpriteRenderer>();
            StartCoroutine(flash.FadeOutRoutine(spriteRenderer, fadeOutDelay));
            foreach (var spriteRenderChild in newWeapon.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(flash.FadeOutRoutine(spriteRenderChild, fadeOutDelay));
            }
        }

        private void ToggleActiveSlot(int numberPressed)
        {
            ToggleActiveHighlight(numberPressed - 1);
        }

        private void ToggleActiveHighlight(int indexNumber)
        {
            activeSlotIndexNum = indexNumber;

            foreach (Transform activeInventorySlot in this.transform)
            {
                activeInventorySlot.GetChild(0).gameObject.SetActive(false);
                activeInventorySlot.gameObject.GetComponent<Image>().enabled = true;
            }

            this.transform.GetChild(indexNumber).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(indexNumber).gameObject.GetComponent<Image>().enabled = false;

            ChangeActiveWeapon();
        }

        private void ChangeActiveWeapon()
        {
            if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
            {
                Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
            }

            Transform childTransform = transform.GetChild(activeSlotIndexNum);
            InventorySlot inventorySlot = childTransform.GetComponent<InventorySlot>();
            WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();


            if (weaponInfo == null)
            {
                ActiveWeapon.Instance.NoWeaponSelected();
                return;
            }

            GameObject weaponToSpawn = weaponInfo.weaponPrefab;
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

            newWeapon.transform.parent = ActiveWeapon.Instance.transform;

            ActiveWeapon.Instance.SelectNewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        }
    }
}