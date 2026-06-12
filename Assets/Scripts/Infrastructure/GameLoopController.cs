using System;
using Behaviours.Enemies;
using Behaviours.Grid;
using UnityEngine;

namespace Infrastructure
{
    public class GameLoopController : MonoBehaviour,IService
    {
        [SerializeField]
        private GridMap _gridMap;
        [SerializeField]
        private EnemySpawnerController _spawnerController;

        private void Start()
        {
            _gridMap.Init();
            _spawnerController.Init();
        }
    }
}