using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MyHeroWay
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField] Image fillbar;
        [SerializeField] Image shrinkBar;
        [SerializeField] public float shrinkTime;
        [SerializeField] public Text statusText;
        public void InitializeBar(bool doAnimation)
        {
            if (doAnimation)
            {
                gameObject.SetActive(true);
                fillbar.fillAmount = 0;
                fillbar.DOFillAmount(1, 1f);
                if (shrinkBar != null)
                {
                    shrinkBar.DOFillAmount(1, 1f);
                    shrinkBar.fillAmount = 0;
                }
            }
            else
            {
                fillbar.fillAmount = 1;
                shrinkBar.fillAmount = 1;
            }
        }

        public void UpdateStatusText(string content)
        {
            if (statusText != null)
            {      
                statusText.text = content;
            }
        }

        public void ResetFillBar()
        {
            fillbar.fillAmount = 1;
            shrinkBar.fillAmount = 1;
        }
        public void UpdateFillBar(float normalizeAmount)
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
            fillbar.DOFillAmount(normalizeAmount, shrinkTime).OnComplete(() =>
            {
                shrinkBar.DOFillAmount(normalizeAmount, .5f);
            });
        }

        public void ShowStatusText(int current, int max)
        {
            statusText.text = current + "/" + max;
        }

        public void Deactive()
        {
            gameObject.SetActive(false);
        }
    }
}

