using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class ExpOrbBar : MonoBehaviour
    {
        [SerializeField] RawImage fillbar;
        public string propertyName = "_Progress";
        [SerializeField] public float shrinkTime;
        [SerializeField] public Text statusText;
        [SerializeField] public Text levelText;
        CharacterExp exp;
        public void InitializeBar()
        {

            exp = PlayerControlManager.Instance.playerController.characterExp;
            levelText.text = exp.CurrentLevel.ToString();
            UpdateStatusText();
            _ = UpdateFillBar();

        }

        public void UpdateStatusText()
        {
            if (statusText != null)
            {
                statusText.text = $"{exp.GetPercentComplete()} %";
            }
            levelText.text = exp.CurrentLevel.ToString();

        }

        public void ResetFillBar()
        {
            fillbar.materialForRendering.SetFloat(propertyName, 0);
        }

        public async UniTask UpdateFillBar()
        {
            if (exp == null || fillbar == null || fillbar.materialForRendering == null)
                return; // Check if exp, fillbar, or fillbar's material is null and return if so

            float normalizeAmount = Mathf.Clamp01(exp.GetNomalize());
            gameObject.SetActive(true);

            float current = fillbar.materialForRendering.GetFloat(propertyName);
            float timer = 0;

            while (timer < shrinkTime)
            {
                if (exp == null || fillbar == null || fillbar.materialForRendering == null)
                    return; // Check if exp, fillbar, or fillbar's material is null during the loop and return if so

                timer += Time.deltaTime;
                float progress = timer / shrinkTime;
                fillbar.materialForRendering.SetFloat(propertyName, Mathf.Lerp(current, normalizeAmount, progress));

                await UniTask.Yield();
            }
        }


        public void Deactive()
        {
            gameObject.SetActive(false);
        }
    }
}
