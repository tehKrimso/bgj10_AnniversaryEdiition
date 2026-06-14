using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Behaviours;
using Behaviours.Enemies;
using Behaviours.Grid;
using UnityEngine;

namespace Infrastructure
{
    public class GameLoopController : MonoBehaviour,IService
    {
        [SerializeField]
        private FightCyclesSettings _fightCyclesSettings;
        [SerializeField]
        private GridMap _gridMap;
        [SerializeField]
        private EnemySpawnerController _spawnerController;
        
        private TowerManager _towerManager;

        private int _currentCycleIndex;
        private bool _cycleRunning;
        private List<BaseEnemy> _activeEnemies;

        private int _cycleEnemiesCount;

        private void Start()
        {
            _activeEnemies = new List<BaseEnemy>();
            _gridMap.Init();
            _spawnerController.Init(_gridMap, _activeEnemies);

            _towerManager = Bootstrapper.Instance.Services.Resolve<TowerManager>();
            _towerManager.Init(_gridMap);
            
            _towerManager.ValidateAllowedTowers(_fightCyclesSettings.fightCycles[_currentCycleIndex].allowedTowerTypes);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.T) && !_cycleRunning)
            {
                Debug.Log("Started");
                StartNextCycle();
                
            }

            if (_cycleRunning && _activeEnemies.Count == 0)
            {
                Debug.Log("No enemies found");
            }

            if (_cycleRunning && _cycleEnemiesCount <= 0)
            {
                OnCycleEnd();
            }
            
        }
        
        public bool IsCycleRunning() => _cycleRunning;
        
        public TileBase GetCenterTile() => _gridMap.GetCenterTile();

        public List<TileBase> GetFreeTilesToBuild()
        {
            var tiles = _gridMap.GetBuildableTiles().Where(t => !t.IsOccupied).ToList();
            foreach (var tile in tiles)
            {
                tile.SetBuildableIndicator(true);
            }
            
            return tiles;
        }

        public void GameLost()
        {
            StopAllCoroutines();
            _towerManager.SetTowersActive(false);
            foreach (BaseEnemy enemy in _activeEnemies)
            {
                enemy.StopMovement();
            }
            Debug.Log("Game Lost");
        }

        public void GameWin()
        {
            Debug.Log("Game Win");
        }

        private void StartNextCycle()
        {
            var cycle = _fightCyclesSettings.fightCycles[_currentCycleIndex];
            _spawnerController.SetActiveRoads(cycle.activeRoadsCount);
            
            StartCoroutine(RunCycle(cycle));
        }

        private IEnumerator RunCycle(FightCycle cycle)
        {
            OnCycleStart();
            foreach (var pack in cycle.enemyPacks)
            {
                _cycleEnemiesCount += pack.enemyCount;
            }
            
            foreach (var pack in cycle.enemyPacks)
            {
                
                StartCoroutine(_spawnerController.StartPackSpawning(pack, _spawnerController.GetFreeSpawner()));

                yield return new WaitForSeconds(cycle.delayBetweenPackSpawns);
            }
            
            //OnCycleEnd();
        }

        private void OnCycleStart()
        {
            _cycleRunning = true;
            _towerManager.SetTowersActive(true);
        }

        private void OnCycleEnd()
        {
            _cycleRunning = false;
            _currentCycleIndex++;
            _towerManager.SetTowersActive(false);

            if (_currentCycleIndex >= _fightCyclesSettings.fightCycles.Count)
            {
                GameWin();
                return;
            }
            
            _towerManager.
                ValidateAllowedTowers(_fightCyclesSettings.fightCycles[_currentCycleIndex].allowedTowerTypes);
        }

        public void RemoveActiveEnemy(BaseEnemy component)
        {
            _cycleEnemiesCount--;
            _activeEnemies.Remove(component);
            _towerManager.RemoveEnemyFromTargetList(component);
            Destroy(component.gameObject);
        }

        public List<BaseEnemy> GetActiveEnemies()
        {
            return _activeEnemies;
        }

        public List<TileBase> GetNewPath(TileBase currentSpawnerTile)
        {
            return _spawnerController.GetDifferentPath(currentSpawnerTile);
        }
    }
}