using AdventureOfZoldan.SceneManagement;
using UnityEngine;

namespace AdventureOfZoldan.UI
{
    public class GameMenuPresenter : MonoBehaviour
    {
        [SerializeField] private KeyCode gameMenuKeycode = KeyCode.Escape;
        private GameObject gameMenu;


        private void Start()
        {
            SceneManager.Instance.onShowGameMenu += ShowGameMenu;
            gameMenu = GameObject.FindWithTag("GameMenu");
            gameMenu.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(gameMenuKeycode))
            {
                DisableGameMenu();
            }
        }

        public void ResumeGame()
        {
            DisableGameMenu();
        }

        public void SaveGame()
        {
            SavingWrapper.Instance.Save();
            DisableGameMenu();
        }

        public void LoadGame()
        {
            SavingWrapper.Instance.Load();
            DisableGameMenu();
        }

        public void ExitGame()
        {
            SceneManager.Instance.QuitGame();
        }

        private void ShowGameMenu()
        {
            if (!gameMenu.activeSelf)
            {
                gameMenu.SetActive(true);
                SceneManager.Instance.PauseGame();
            }
        }

        private void DisableGameMenu()
        {
            gameMenu.SetActive(false);
            SceneManager.Instance.ResumeGame();
        }
    }
}