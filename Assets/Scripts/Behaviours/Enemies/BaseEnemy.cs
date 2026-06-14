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
        protected bool _shouldMove = true;
        private bool _isSlowed = false;
        private bool _isFreezed = false;
        
        protected List<TileBase> _waypoints;
        protected int _currentWaypointIndex;
        protected GameLoopController _gameLoopController;

        private float _freezeTimer;
        private float _slowTimer;
        private float _slowRate;

        protected void Start()
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

        protected void Update()
        {
            
            
            if (_waypoints == null || _waypoints.Count == 0)
                return;

            if (_currentWaypointIndex >= _waypoints.Count)
                return;
            
            if(_isSlowed)
                CheckSlow();
            
            if(_isFreezed)
                CheckFreeze();

            if (_shouldMove)
            {
                Vector3 targetPos = _waypoints[_currentWaypointIndex].PointToMoveTo.position;
            
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * (1 - _slowRate) * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
                {
                    _currentWaypointIndex++;
                }
            }
        }

        public void ApplySlow(float slowRate, float slowTime)
        {
            Debug.Log("Applying slow");
            _slowRate = slowRate;
            _slowTimer = slowTime;
            _isSlowed = true;
        }

        public void ApplyFreeze(float freezeTime)
        {
            _freezeTimer = freezeTime;
            _shouldMove = false;
            _isFreezed = true;
        }

        private void CheckSlow()
        {
            _slowTimer -= Time.deltaTime;
            if (_slowTimer <= 0)
            {
                _slowTimer = 0f;
                _isSlowed = false;
            }
        }

        private void CheckFreeze()
        {
            _freezeTimer -= Time.deltaTime;
            if (_freezeTimer <= 0)
            {
                _isFreezed = false;
                _shouldMove =  true;
            }
        }
    }
}
