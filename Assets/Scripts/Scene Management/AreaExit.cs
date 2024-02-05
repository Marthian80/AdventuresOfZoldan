using AdventureOfZoldan.Player;
using AdventureOfZoldan.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
                wrapper.Save();

                UIFade.Instance.FadeToBlack();

                StartCoroutine(LoadSceneRoutine());

                //Load current level            

            }
        }

        private IEnumerator LoadSceneRoutine()
        {
            yield return new WaitForSeconds(sceneLoadTime);
            SceneManager.LoadScene(sceneToLoad);
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        }
    }
}