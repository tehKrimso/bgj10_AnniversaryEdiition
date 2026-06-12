using System;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        public static Bootstrapper Instance { get; private set; }

        public ServiceLocator Services;

        private void Awake()
        {
            Instance = this;
            
            Services = new ServiceLocator();
            RegisterServices();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void RegisterServices()
        {
            Services.Register<GameLoopController>(new GameLoopController());
        }
    }
}
