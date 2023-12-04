using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            SceneManager.LoadScene(sceneToLoad);
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        }
    }
}