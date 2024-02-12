namespace MyHeroWay
{
    using UnityEngine;

    public class SpriteBar : MonoBehaviour
    {
        [SerializeField] Transform fillbar;
        [SerializeField] Transform shrinkBar;
        public float shrinkTime;
        float shrinkTimer;
        float deactiveTimer;

        private void Start()
        {
            shrinkTimer = shrinkTime;
        }
        public void UpdateBar(float normalizeAmount)
        {
            if (normalizeAmount <= 0)
            {
                normalizeAmount = 0;
                shrinkTimer = 0.1f;
            }
            if (normalizeAmount >= 1)
            {
                normalizeAmount = 1;
            }
            gameObject.SetActive(true);
            fillbar.localScale = new Vector3(normalizeAmount, fillbar.localScale.y, fillbar.localScale.z);
            deactiveTimer = 3f;
        }

        private void Update()
        {
            if (deactiveTimer > 0)
            {
                deactiveTimer -= Time.deltaTime;
                if (deactiveTimer <= 0)
                {
                    Deactive();
                }
            }
            if (shrinkBar == null) return;
            if (shrinkTimer > 0)
            {
                shrinkTimer -= Time.deltaTime;
            }
            else
            {
                shrinkTimer = shrinkTime;
                float newValueX = Mathf.Lerp(shrinkBar.transform.localScale.x, fillbar.transform.localScale.x, 0.1f);
                shrinkBar.localScale = new Vector3(newValueX, shrinkBar.localScale.y, shrinkBar.localScale.z);
            }
        }
        public void Deactive()
        {
            gameObject.SetActive(false);
        }
    }

}
