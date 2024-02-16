using AdventureOfZoldan.Core;
using AdventureOfZoldan.Player;
using AdventureOfZoldan.UI;
using UnityEngine;

namespace AdventureOfZoldan.SceneManagement
{
    public class AreaEntrance : MonoBehaviour
    {
        [SerializeField] private string sceneTransitionName;

        private void Start()
        {
            if (sceneTransitionName == SceneManager.Instance.SceneTransitionName)
            {
                SavingWrapper.Instance.Load();

                PlayerController.Instance.transform.position = this.transform.position;
                CameraController.Instance.SetPlayerCameraFollow();

                SavingWrapper.Instance.Save();

                UIFade.Instance.FadeToClear();
            }
        }
    }
}
