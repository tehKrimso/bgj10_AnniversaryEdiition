using System;
using System.Collections;
using System.Collections.Generic;
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

        private int _currentCycleIndex;
        private bool _cycleRunning;
        private List<BaseEnemy> _activeEnemies;

        private void Start()
        {
            _activeEnemies = new List<BaseEnemy>();
            _gridMap.Init();
            _spawnerController.Init(_gridMap, _activeEnemies);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.T) && !_cycleRunning)
            {
                Debug.Log("Started");
                StartNextCycle();
                
            }

            if (_activeEnemies.Count == 0)
            {
                Debug.Log("No enemies found");
            }
            
        }

        private void StartNextCycle()
        {
            var cycle = _fightCyclesSettings.fightCycles[_currentCycleIndex];
            _spawnerController.SetActiveRoads(cycle.activeRoadsCount);
            
            var cycleCoroutine = StartCoroutine(RunCycle(cycle));
            
            // var pack = cycle.enemyPacks[_currentPackIndex];
            // _currentCycleIndex++;
            // _spawnerController.StartPackSpawning(pack);
        }

        private IEnumerator RunCycle(FightCycle cycle)
        {
            _cycleRunning = true;
            foreach (var pack in cycle.enemyPacks)
            {
                
                StartCoroutine(_spawnerController.StartPackSpawning(pack, _spawnerController.GetFreeSpawner()));

                yield return new WaitForSeconds(cycle.delayBetweenPackSpawns);
            }
            _cycleRunning = false;
            _currentCycleIndex++;
        }
    }
}