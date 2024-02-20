using MyHeroWay;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementDictionary", menuName = "ElementDictionary", order = 3)]
public class ElementDictionary : ScriptableObject
{
    public ElementType[] data;

    public Sprite GetSprite(EElementType type)
    {
       return data.FirstOrDefault(e => e.element == type).icon; 
    }
}

[System.Serializable]
public class ElementType
{
    public EElementType element;
    public Sprite icon;
}