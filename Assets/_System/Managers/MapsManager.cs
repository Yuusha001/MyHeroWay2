using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;

namespace MyHeroWay
{
    public class MapsManager : Singleton<MapsManager>
    {
        public EMapLink previusMap;
        public EMapLink currentMap;
        public List<MapGame> mapGames;
        protected override void Awake()
        {
            base.Awake();
            mapGames = new List<MapGame>();
        }

        private async UniTask<MapGame> LoadMapAsync(EMapLink mapLink)
        {
            bool res = false;

            foreach (var item in mapGames)
            {
                if (item.mapName == mapLink)
                {
                    res = true;
                    item.gameObject.SetActive(true);
                    return item;
                }
            }

            if (!res)
            {
                var operation = Resources.LoadAsync<MapGame>($"Prefabs/Maps/{mapLink}");
                await operation;
                // Instantiate the loaded map
                MapGame mapGame = Instantiate(operation.asset as MapGame, this.transform);
                mapGame.mapName = mapLink;
                mapGames.Add(mapGame);
                return mapGame;
            }
            return null;
        }

        public async void Initialize(UserData data)
        {
            PlayerControlManager.Instance.transform.position = data.mapData.currentPos;
            currentMap = data.mapData.currentMap;
            previusMap = EMapLink.None;

            MapGame mapGame = await LoadMapAsync(currentMap);
            CameraManager.Instance.mapBorder.m_BoundingShape2D = mapGame.border;
            
        }

        public async void SetPlayerPosition(EMapLink mapLink)
        {
            previusMap = currentMap;

            // mapLink - previusMap = currentMap (bitwise)
            currentMap = mapLink & ~previusMap;
            MapGame mapGame = await LoadMapAsync(currentMap);
            CameraManager.Instance.mapBorder.m_BoundingShape2D = mapGame.border;

            //PlayerControlManager.Instance.transform.position = 

        }
    }
}
