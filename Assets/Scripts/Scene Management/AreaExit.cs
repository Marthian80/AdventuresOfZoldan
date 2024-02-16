using AdventureOfZoldan.Player;
using AdventureOfZoldan.UI;
using System.Collections;
using UnityEngine;

namespace AdventureOfZoldan.SceneManagement
{
    public class AreaExit : MonoBehaviour
    {
        [SerializeField] private string sceneTransitionName;
        [SerializeField] private string sceneToLoad;
        [SerializeField] private float sceneLoadTime = 1f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                //Save current level
                SavingWrapper.Instance.Save();

                UIFade.Instance.FadeToBlack();

                StartCoroutine(LoadSceneRoutine());
            }
        }

        private IEnumerator LoadSceneRoutine()
        {
            yield return new WaitForSeconds(sceneLoadTime);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
            SceneManager.Instance.SetTransitionName(sceneTransitionName);
        }
    }
}