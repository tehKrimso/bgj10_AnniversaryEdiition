using System.Collections.Generic;
using System.Linq;
using Behaviours.Grid;
using Infrastructure;
using UnityEngine;

namespace Behaviours.Enemies
{
    public class EnemySpawnerController : MonoBehaviour
    {
        private List<EnemySpawner> _spawners;
        private EnemiesFactory _enemiesFactory;
        public void Init(GridMap gridMap)
        {
            _spawners = FindObjectsOfType<EnemySpawner>().ToList();
            _enemiesFactory = Bootstrapper.Instance.Services.Resolve<EnemiesFactory>();

            foreach (var spawner in _spawners)
            {
                spawner.pathToCenter = gridMap.GetPathFromTileToCenter(spawner.parentTile);
            }
        }
    }
}