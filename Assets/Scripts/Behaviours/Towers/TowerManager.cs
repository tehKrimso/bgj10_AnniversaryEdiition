using System;
using System.Collections.Generic;
using System.Linq;
using Behaviours.Enemies;
using Behaviours.Grid;
using Infrastructure;

namespace Behaviours
{
    public class TowerManager : IService
    {
        private TowersFactory _factory;
        private GridMap _gridMap;
        
        private Random _random;

        public Dictionary<TowerType, BaseTower> Towers = new Dictionary<TowerType, BaseTower>();
        public TowerManager()
        {
            _factory = Bootstrapper.Instance.Services.Resolve<TowersFactory>();
            _random = new Random();
        }

        public void Init(GridMap gridMap)
        {
            _gridMap = gridMap;
        }

        public void ValidateAllowedTowers(List<TowerType> allowedTowers)
        {
            var buildableTiles = _gridMap.GetBuildableTiles();
            buildableTiles = buildableTiles.Where(t => !t.IsOccupied).ToList();
            foreach (var towerType in allowedTowers)
            {
                if (!Towers.ContainsKey(towerType))
                {
                    int randomTileIndex = _random.Next(buildableTiles.Count);
                    var tileForNewTower = buildableTiles[randomTileIndex];
                    var newTower = _factory.SpawnTower<BaseTower>(towerType, tileForNewTower);
                    newTower.SetNewTowerIndicator(true);
                    Towers.Add(towerType, newTower);
                    buildableTiles.Remove(tileForNewTower);
                }
            }
        }

        public void SetTowersActive(bool active)
        {
            foreach (var tower in Towers.Values)
            {
                tower.SetActive(active);
            }
        }

        public void RemoveEnemyFromTargetList(BaseEnemy enemy)
        {
            foreach (var tower in Towers.Values)
            {
                tower.RemoveEnemyFromTargetList(enemy);
            }
        }
    }
}