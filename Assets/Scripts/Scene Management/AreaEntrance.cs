using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string sceneTransitionName;

    private void Start()
    {
        if (sceneTransitionName == SceneManagement.Instance.SceneTransitionName) 
        {
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();

            UIFade.Instance.FadeToClear();
        }
    }
}
