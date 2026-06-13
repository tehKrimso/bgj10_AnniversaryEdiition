using System;
using System.Collections.Generic;
using Behaviours.Grid;
using Infrastructure;
using UnityEngine;

namespace Behaviours.Enemies
{
    public class BaseEnemy : MonoBehaviour
    {
        public int DamageToCoreBuilding = 1;
        
        public int initialHealth;
        public float moveSpeed;
        
        public bool IsDead => _currentHealth <= 0;
        
        private int _currentHealth;
        private bool _shouldMove = true;
        
        private List<TileBase> _waypoints;
        private int _currentWaypointIndex;
        private GameLoopController _gameLoopController;

        private void Start()
        {
            _currentHealth = initialHealth;
            _gameLoopController = Bootstrapper.Instance.Services.Resolve<GameLoopController>();
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (IsDead)
            {
                _gameLoopController.RemoveActiveEnemy(this);
            }
        }
        
        //give waypoints on spawn
        //move between waypoints
        public void SetPath(List<TileBase> spawnerPathToCenter)
        {
            _waypoints = spawnerPathToCenter;
        }

        public void StopMovement()
        {
            _shouldMove = false;
        }

        private void Update()
        {
            
            
            if (_waypoints == null || _waypoints.Count == 0)
                return;

            if (_currentWaypointIndex >= _waypoints.Count)
                return;

            if (_shouldMove)
            {
                Vector3 targetPos = _waypoints[_currentWaypointIndex].PointToMoveTo.position;
            
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
                {
                    _currentWaypointIndex++;
                }
            }
        }
    }
}
