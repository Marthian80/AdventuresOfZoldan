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
                uiContainer.SetActive(!uiContainer.activeSelf);
            }
        }
    }
}