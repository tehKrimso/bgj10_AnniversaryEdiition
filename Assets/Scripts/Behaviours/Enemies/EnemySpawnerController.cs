using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Behaviours.Grid;
using Infrastructure;
using UnityEngine;
using Random = System.Random;

namespace Behaviours.Enemies
{
    public class EnemySpawnerController : MonoBehaviour
    {
        private List<EnemySpawner> _spawners;
        private EnemiesFactory _enemiesFactory;
        private Random _random;
        private List<BaseEnemy> _activeEnemies;

        public void Init(GridMap gridMap, List<BaseEnemy> activeEnemies)
        {
            _spawners = FindObjectsOfType<EnemySpawner>().ToList();
            _enemiesFactory = Bootstrapper.Instance.Services.Resolve<EnemiesFactory>();
            _random = new Random();
            _activeEnemies = activeEnemies;

            foreach (var spawner in _spawners)
            {
                spawner.pathToCenter = gridMap.GetPathFromTileToCenter(spawner.parentTile);
                spawner.SetActive(false);
            }
        }

        public void SetActiveRoads(int cycleActiveRoadsCount)
        {
            for (int i = 0; i < cycleActiveRoadsCount; i++)
            {
                var disabledSpawners = _spawners.Where(s => !s.enabled).ToList();
                
                disabledSpawners[_random.Next(0, disabledSpawners.Count - 1)].enabled = true;
            }
        }

        public EnemySpawner GetFreeSpawner()
        {
            var freeSpawners = _spawners.Where(s => !s.isOccupide).ToList();
            var spawner = freeSpawners[_random.Next(0, freeSpawners.Count)];
            spawner.isOccupide = true;
            return spawner;
        }

        public IEnumerator StartPackSpawning(EnemyPack pack, EnemySpawner spawner)
        {
            for (int i = 0; i < pack.enemyCount; i++)
            {
                var enemy = _enemiesFactory.SpawnEnemy(pack.enemyType, spawner);
                _activeEnemies.Add(enemy);

                if (i < pack.enemyCount - 1)
                {
                    yield return new WaitForSeconds(pack.delayBetweenSingleEnemySpawn);
                }
            }
            
            spawner.isOccupide = false;
        }
    }
}