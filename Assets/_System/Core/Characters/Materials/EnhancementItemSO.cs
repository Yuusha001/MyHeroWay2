using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    [CreateAssetMenu(fileName = "EnhancementItemSO", menuName = "Item/EhancementItemSO", order = 3)]
    public class EnhancementItemSO : ItemDataSO
    {
        public EElementType elementType;
        [ShowIf("elementType",EElementType.None)]
        public EEnhancementItemType enhancementItemType;
        [ShowIf("elementType",EElementType.None)]
        public int value;
    }
}
