using AdventureOfZoldan.SceneManagement;
using UnityEngine;

namespace AdventureOfZoldan.UI
{
    public class GameOverMenuPresenter : MonoBehaviour
    {
        private GameObject gameOverMenu;

        private void Start()
        {
            SceneManager.Instance.onShowGameOverMenu += ShowGameOverMenu;
            gameOverMenu = GameObject.FindWithTag("GameOverMenu"); 
            gameOverMenu.SetActive(false);
        }

        public void RestartGame()
        {
            SceneManager.Instance.RestartGame();
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

        private void ShowGameOverMenu()
        {
            if (!gameOverMenu.activeSelf)
            {
                gameOverMenu.SetActive(true);
                SceneManager.Instance.PauseGame();
            }                
        }

        private void DisableGameMenu()
        {
            gameOverMenu.SetActive(false);
            SceneManager.Instance.ResumeGame();
        }
    }
}

