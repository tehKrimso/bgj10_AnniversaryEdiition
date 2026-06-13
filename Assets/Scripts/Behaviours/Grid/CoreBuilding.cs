using System;
using Behaviours.Enemies;
using Infrastructure;
using UnityEngine;

namespace Behaviours.Grid
{
    public class CoreBuilding : MonoBehaviour
    {
        public int Health = 100;
        private GameLoopController _gameLoopController;

        private void Start()
        {
            _gameLoopController = Bootstrapper.Instance.Services.Resolve<GameLoopController>();
        }

        private void Update()
        {
            if (Health <= 0)
            {
                _gameLoopController.GameLost();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BaseEnemy enemy))
            {
                Health -= enemy.DamageToCoreBuilding;
                _gameLoopController.RemoveActiveEnemy(enemy);
            }
        }
    }
}
