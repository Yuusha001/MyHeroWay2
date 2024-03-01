using Cysharp.Threading.Tasks;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using Utils.String;

namespace MyHeroWay
{
    public class ItemDrop : MonoBehaviour
    {
        private bool isActive;
        public EItemDrop type;
        private int value;
        private Vector2 pos;
        public ParticleSystem impact;
        [SerializeField] float movementDuration = 3.0f;
        [SerializeField] float percentComplete = 0.0f;
        [SerializeField] AnimationCurve animationCurve;
        [SerializeField] AnimationCurve dropCurve;
        public void Initialize(int value)
        {
            this.value = value;
            gameObject.SetActive(true);
            float time = Random.Range(0.5f, 1f);
            if (type == EItemDrop.Exp)
            {
                _ = Drop(time, Random.Range(-2f, 2f));
                Invoke(nameof(ActiveExp), time);
            }
            else
            {
                _ = Drop(0.2f, Random.Range(-2f, 2f));
                Invoke(nameof(Active), 0.2f);
            }

        }

        private async UniTask Drop(float popDuration, float heightY)
        {
            Vector2 startPoint = transform.position;
            float randomX = transform.position.x + Random.Range(-2f, 2f);
            float randomY = transform.position.y + Random.Range(-1f, 1f);

            Vector2 endPoint = new Vector2(randomX, randomY);

            float timePassed = 0f;

            while (timePassed < popDuration)
            {
                timePassed += Time.deltaTime;
                float linearT = timePassed / popDuration;
                float heightT = dropCurve.Evaluate(linearT);
                float height = Mathf.Lerp(0f, heightY, heightT);

                transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
                await UniTask.Yield();
            }

        }

        private async UniTask Move()
        {
            var startMoveTime = Time.time;
            while (Time.time - startMoveTime < movementDuration)
            {
                percentComplete = (Time.time - startMoveTime) / movementDuration;
                pos = new Vector2(PlayerControlManager.Instance.transform.position.x, PlayerControlManager.Instance.transform.position.y);
                transform.position = Vector3.Lerp(transform.position, pos, animationCurve.Evaluate(percentComplete));
                if (Vector3.Distance(transform.position, pos) <= 0.1f)
                {
                    transform.position = pos;
                    break;
                }
                await UniTask.Yield();
            }
            
            //AudioManager.Instance.PlayOneShot("collect_coin", 0.7f);
            await transform.DOScale(0, 0.3f).SetEase(Ease.OutQuart).AsyncWaitForCompletion();
            DataManager.Instance.expTextEff.Spawn(this.transform.position, value);

        }
        private void ActiveExp()
        {
            isActive = true;
            _ = Move();
        }
        private void Active()
        {
            isActive = true;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isActive) return;
            if (other.CompareTag(StrManager.PlayerTag))
            {
                //AudioManager.Instance.PlayOneShot("eff_item_weapon", 1);
                impact.Play();
                switch (type)
                {
                    case EItemDrop.Diamond:
                        break;
                    case EItemDrop.Item:
                        break;
                    case EItemDrop.SliverKey:
                        break;
                    case EItemDrop.GoldenKey:
                        break;
                    case EItemDrop.Exp:
                        DataManager.Instance.expTextEff.Spawn(this.transform.position, value);
                        break;
                    case EItemDrop.Coin:
                        break;
                    case EItemDrop.Wood:
                        break;
                }
                isActive = false;
                FactoryObject.Despawn(StrManager.VFXPool, this.transform, PoolManager.Pools[StrManager.VFXPool].transform);
            }
        }
    }
}
