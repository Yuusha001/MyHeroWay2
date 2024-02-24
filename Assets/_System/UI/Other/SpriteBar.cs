namespace MyHeroWay
{
    using DG.Tweening;
    using UnityEngine;

    public class SpriteBar : MonoBehaviour
    {
        [SerializeField] Transform fillbar;
        [SerializeField] Transform shrinkBar;
        public float shrinkTime;

        public void InitializeBar()
        {
            gameObject.SetActive(true);
            fillbar.localScale = Vector3.one;
            shrinkBar.localScale = Vector3.one;
        }

        public void UpdateBar(float normalizeAmount)
        {
            if (normalizeAmount <= 0)
            {
                normalizeAmount = 0;
            }
            if (normalizeAmount >= 1)
            {
                normalizeAmount = 1;
            }
            gameObject.SetActive(true);
            fillbar.localScale = new Vector3(normalizeAmount, fillbar.localScale.y, fillbar.localScale.z);
            fillbar.DOScaleX(normalizeAmount, shrinkTime).OnComplete(() =>
            {
                shrinkBar.DOScaleX(normalizeAmount, .5f);
            });
        }
        public void Deactive()
        {
            gameObject.SetActive(false);
        }
    }

}
