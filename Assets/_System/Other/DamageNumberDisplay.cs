using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DamageNumbersPro;

namespace MyHeroWay
{
    public class DamageNumberDisplay : MonoBehaviour
    {
        public List<string> texts;
        public List<TMP_FontAsset> fonts;
        public bool randomColor;

        public void Apply(Transform transform,int damage = 0)
        {
            DamageNumber target = GetComponent<DamageNumber>();
            target.position = transform.position;
            if (texts != null && texts.Count > 0)
            {
                if(damage == 0)
                {
                    int randomIndex = Random.Range(0, texts.Count);
                    target.leftText = texts[randomIndex];
                    if (fonts != null && randomIndex < fonts.Count)
                    {
                        target.SetFontMaterial(fonts[randomIndex]);
                    }
                }
                else
                {
                    target.leftText = damage.ToString();
                }          
            }

            if (randomColor)
            {
                target.SetColor(Color.HSVToRGB(Random.value, 0.5f, 1f));
            }
        }
    }
}
