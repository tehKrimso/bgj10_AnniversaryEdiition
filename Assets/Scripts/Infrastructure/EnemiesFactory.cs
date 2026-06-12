using System.Collections.Generic;
using System.Linq;
using Behaviours.Enemies;
using UnityEngine;

namespace Infrastructure
{
    public class EnemiesFactory : IService
    {
        private EnemyPrefabsSettings _enemyPrefabsSettings;
        
        private Dictionary<EnemyType, GameObject> _enemyPrefabs;

        public EnemiesFactory(EnemyPrefabsSettings settings)
        {
            _enemyPrefabsSettings =  settings;
            _enemyPrefabs = new Dictionary<EnemyType, GameObject>();
            foreach (PrefabForEnemyType prefabForEnemyType in _enemyPrefabsSettings.enemyPrefabs)
            {
                _enemyPrefabs[prefabForEnemyType.enemyType] = prefabForEnemyType.prefab;
            }
        }

        public BaseEnemy SpawnEnemy(EnemyType type, EnemySpawner spawner)
        {
            var enemy = GameObject.Instantiate(_enemyPrefabs[type], spawner.transform.position, spawner.transform.rotation);
            return enemy.GetComponent<BaseEnemy>();
        }
    }
}