using AdventureOfZoldan.Core;
using System;
using UnityEngine;

namespace AdventureOfZoldan.SceneManagement
{
    public class SceneManager : Singleton<SceneManager>
    {
        [SerializeField] private string FirstScene = string.Empty;
        [SerializeField] private KeyCode gameMenuKeycode = KeyCode.Escape;

        public event Action onShowGameMenu;
        public event Action onShowGameOverMenu;
        public event Action onGameRestarted;

        public string SceneTransitionName { get; private set; }
        public bool GamePaused { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (Input.GetKeyDown(gameMenuKeycode))
            {
                if (onShowGameMenu != null)
                {
                    onShowGameMenu();
                }
            }
        }

        public void RestartGame()
        {
            if (onGameRestarted != null)
            {
                onGameRestarted();
            }
            ResumeGame();
            UnityEngine.SceneManagement.SceneManager.LoadScene(FirstScene);            
        }

        public void SetTransitionName(string sceneTransitionName)
        {
            this.SceneTransitionName = sceneTransitionName;
        }

        public void ShowGameOverScreen()
        {
            if (onShowGameOverMenu != null)
            {
                onShowGameOverMenu();                
            }
        }

        public void PauseGame()
        {
            GamePaused = true;
        }

        public void ResumeGame()
        {
            GamePaused = false;
        }

        public void QuitGame()
        {            
            Application.Quit();
        }        
    }
}