using System.Collections;
using UnityEngine;

namespace AdventureOfZoldan.Misc
{
    public class Flash : MonoBehaviour
    {
        [SerializeField] private Material whiteFlashMat;
        [SerializeField] private float flashTime = 0.15f;

        private Material defaultMat;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultMat = spriteRenderer.material;
        }

        public float GetFlashTime()
        {
            return flashTime;
        }

        public IEnumerator FlashRoutine()
        {
            spriteRenderer.material = whiteFlashMat;
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.material = defaultMat;
        }
    }
}