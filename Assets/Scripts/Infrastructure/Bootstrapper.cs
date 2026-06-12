using System;
using Behaviours.Grid;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Grid Settings")]
        public TilePrefabsSettings  tilePrefabs;
        public MapTileLayoutSettings  gridLayoutSettings;
        
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
            Services.Register<GameLoopController>(new GameLoopController());
            Services.Register<TilesFactory>(new TilesFactory(tilePrefabs, gridLayoutSettings));
            Services.Register<PlayerInputService>(new PlayerInputService());
        }
    }
}
