
using System;

namespace MyHeroWay
{
    [Flags]
    public enum EMapLink
    {
        None = 0,
        BeginerVillage = 1,
        Farm = 1 << 1,
        Near_BeginerForest_A = 1 << 2,
        Near_BeginerForest_B = 1 << 3,
    }
}
