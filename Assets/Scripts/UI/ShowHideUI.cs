using AdventureOfZoldan.SceneManagement;
using UnityEngine;


namespace AdventureOfZoldan.UI
{
    public class ShowHideUI : MonoBehaviour
    {       
        [SerializeField] private KeyCode toggleKey;
        [SerializeField] private GameObject uiContainer = null;
                
        void Start()
        {            
            uiContainer.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                if (!uiContainer.activeSelf)
                {
                    SceneManager.Instance.InventoryScreenActivated();
                }
                else
                {
                    SceneManager.Instance.InventoryScreenClosed();
                }

                uiContainer.SetActive(!uiContainer.activeSelf);
            }
        }
    }
}