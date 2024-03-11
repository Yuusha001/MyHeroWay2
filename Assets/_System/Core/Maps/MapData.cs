using UnityEngine;

namespace MyHeroWay
{
    [System.Serializable]
    public class MapData
    {
        public EMapLink currentMap;
        public Vector2 currentPos;

        public MapData()
        {
            currentMap = EMapLink.BeginerVillage;
            currentPos = new Vector2(-6.5f,-2f);
        }
    }
}
