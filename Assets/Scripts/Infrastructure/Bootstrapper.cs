using System;
using Behaviours;
using Behaviours.Data;
using Behaviours.Enemies;
using Behaviours.Grid;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Managers")]
        public GameLoopController gameLoopController;
        
        [Header("Grid Settings")]
        public TilePrefabsSettings  tilePrefabs;
        public MapTileLayoutSettings  gridLayoutSettings;

        [Header("Enemies Settings")] 
        public EnemyPrefabsSettings enemyPrefabsSettings;
        
        [Header("Tower Settings")]
        public TowerPrefabSettings towerPrefabSettings;
        
        public static Bootstrapper Instance { get; private set; }

        public ServiceLocator Services;

        private void Awake()
        {
            Instance = this;
            
            Services = new ServiceLocator();
            RegisterServices();
        }

        private void RegisterServices()
        {
            Services.Register<GameLoopController>(gameLoopController);
            Services.Register<TilesFactory>(new TilesFactory(tilePrefabs, gridLayoutSettings));
            Services.Register<EnemiesFactory>(new EnemiesFactory(enemyPrefabsSettings));
            Services.Register<TowersFactory>(new TowersFactory(towerPrefabSettings));
            Services.Register<PlayerInputService>(new PlayerInputService());
            Services.Register<TowerManager>(new TowerManager());
        }
    }
}
