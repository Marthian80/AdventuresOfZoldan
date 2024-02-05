using AdventureOfZoldan.Player;
using Cinemachine;

namespace AdventureOfZoldan.Core
{
    public class CameraController : Singleton<CameraController>
    {
        private CinemachineVirtualCamera cinemachineVirtualCamera;

        public void SetPlayerCameraFollow()
        {
            cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
        }
    }
}