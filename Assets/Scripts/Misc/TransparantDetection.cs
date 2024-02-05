using AdventureOfZoldan.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AdventureOfZoldan.Misc
{
    public class TransparantDetection : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float transparancyAmount = 0.8f;
        [SerializeField] private float transparancyFadeTime = 0.3f;

        private SpriteRenderer spriteRenderer;
        private Tilemap tilemap;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            tilemap = GetComponent<Tilemap>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                if (spriteRenderer)
                {
                    StartCoroutine(FadeRoutine(spriteRenderer, transparancyFadeTime, spriteRenderer.color.a, transparancyAmount));
                }
                else if (tilemap)
                {
                    StartCoroutine(FadeRoutine(tilemap, transparancyFadeTime, tilemap.color.a, transparancyAmount));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                if (spriteRenderer)
                {
                    StartCoroutine(FadeRoutine(spriteRenderer, transparancyFadeTime, spriteRenderer.color.a, 1f));
                }
                else if (tilemap)
                {
                    StartCoroutine(FadeRoutine(tilemap, transparancyFadeTime, tilemap.color.a, 1f));
                }
            }
        }

        private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float TargetTransparancy)
        {
            float elapsedTime = 0;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, TargetTransparancy, elapsedTime / fadeTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
                yield return null;
            }
        }

        private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float TargetTransparancy)
        {
            float elapsedTime = 0;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, TargetTransparancy, elapsedTime / fadeTime);
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
                yield return null;
            }
        }
    }
}