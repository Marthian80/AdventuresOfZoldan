using System;
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

        public IEnumerator FadeOutRoutine(SpriteRenderer externalSpriteRenderer, float fadeTime)
        {
            float elapsedTime = 0;
            float startValue = externalSpriteRenderer.material.color.a;
            var color = externalSpriteRenderer.color;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);
                externalSpriteRenderer.color = new Color(color.r, color.b, color.g, newAlpha);
                yield return null;
            }
        }

        public IEnumerator FadeInRoutine(SpriteRenderer externalSpriteRenderer, float fadeTime)
        {
            float elapsedTime = 0;
            float startValue = externalSpriteRenderer.material.color.a;
            var color = externalSpriteRenderer.color;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(0f, startValue, elapsedTime / fadeTime);
                externalSpriteRenderer.color = new Color(color.r, color.b, color.g, newAlpha);
                yield return null;
            }
        }


    }
}