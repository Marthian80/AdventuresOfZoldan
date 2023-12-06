using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float sceneLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            UIFade.Instance.FadeToBlack();

            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(sceneLoadTime);
        SceneManager.LoadScene(sceneToLoad);
        SceneManagement.Instance.SetTransitionName(sceneTransitionName);
    }
}
