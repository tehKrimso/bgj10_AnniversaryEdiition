using System.Collections.Generic;
using Behaviours;
using Behaviours.Data;
using Behaviours.Grid;
using UnityEngine;

namespace Infrastructure
{
    public class TowersFactory : IService
    {
        private TowerPrefabSettings _settings;
        private Dictionary<TowerType, GameObject> _towerPrefabs;

        public TowersFactory(TowerPrefabSettings settings)
        {
            _settings = settings;
            _towerPrefabs = new Dictionary<TowerType, GameObject>();
            foreach (var towerSettings in settings.towerPrefabs)
            {
                _towerPrefabs[towerSettings.towerType] = towerSettings.prefab;
            }
        }

        public TTowerType SpawnTower<TTowerType>(TowerType towerType, TileBase tileToPlace)  where TTowerType : BaseTower
        {
            var towerGameObject = GameObject.Instantiate(_towerPrefabs[towerType]);
            var towerComponent = towerGameObject.GetComponent<TTowerType>();
            tileToPlace.SetPlacedTower(towerComponent);
            
            return towerComponent;
        }
    }
}