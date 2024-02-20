using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.String;

namespace MyHeroWay
{
    public class LookAble : CoreComponent
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(StrManager.LookAbleTag))
            {
                if (collision.TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    _ = FadeRoutine(spriteRenderer, .4f, spriteRenderer.color.a, .8f);
                }
                if (collision.TryGetComponent(out Tilemap tilemap))
                {
                    _ = FadeRoutine(tilemap, .4f, tilemap.color.a, .8f);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(StrManager.LookAbleTag))
            {
                if (collision.TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    _ = FadeRoutine(spriteRenderer, .4f, spriteRenderer.color.a, 1);
                }
                if (collision.TryGetComponent(out Tilemap tilemap))
                {
                    _ = FadeRoutine(tilemap, .4f, tilemap.color.a, 1);
                }
            }
        }


        private async UniTask FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
        {
            float elapsedTime = 0;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
                await UniTask.Yield();
            }
        }

        private async UniTask FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
        {
            float elapsedTime = 0;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
                await UniTask.Yield();
            }
        }
    }
}
