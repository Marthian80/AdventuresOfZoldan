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
            if (sceneTransitionName == SceneManagement.Instance.SceneTransitionName)
            {
                SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
                wrapper.Load();

                PlayerController.Instance.transform.position = this.transform.position;
                CameraController.Instance.SetPlayerCameraFollow();

                wrapper.Save();

                UIFade.Instance.FadeToClear();
            }
        }
    }
}
