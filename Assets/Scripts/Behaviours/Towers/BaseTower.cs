using System;
using Behaviours.Enemies;
using Behaviours.Grid;
using Infrastructure;
using UnityEngine;

namespace Behaviours
{
    public abstract class BaseTower : MonoBehaviour
    {
        public TowerType towerType;
        public TowerParameters parameters;
        public TileBase parentTile;
        public TowerTargetFinder targetFinder;

        protected GameLoopController _gameLoopController;
        protected float _attackTimer;
        protected bool _isActive;
        protected Vector3 _centerTilePosition;

        protected float _abilityCooldownTimer;
        protected bool _abilityOnCooldown;

        protected void Start()
        {
            _gameLoopController = Bootstrapper.Instance.Services.Resolve<GameLoopController>();
            _centerTilePosition = _gameLoopController.GetCenterTile().gameObject.transform.position;
        }

        protected void Update()
        {
            if(!_isActive)
                return;
            
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= parameters.AttackCooldown)
            {
                PerformBasicAttack();
                _attackTimer = 0f;
            }
        }
        
        public void SetActive(bool active) => _isActive = active;

        public void SetNewTowerIndicator(bool isActive)
        {
            //show new tower indicator
            //hide on first click
            //throw new NotImplementedException();
        }

        public abstract void PerformBasicAttack();

        public abstract void PerformAbility();

        public void RemoveEnemyFromTargetList(BaseEnemy enemy)
        {
            targetFinder.RemoveFromPotentialTargets(enemy);
        }

        protected void StartAbilityCooldownTimer()
        {
            _abilityCooldownTimer = parameters.AbilityCooldown;
            _abilityOnCooldown = true;
        }

        private void CheckAbilityCooldown()
        {
            if (_abilityOnCooldown)
            {
                _abilityCooldownTimer -= Time.deltaTime;
                if (_abilityCooldownTimer <= 0)
                {
                    _abilityOnCooldown = false;
                }
            }
        }
    }

    [Serializable]
    public class TowerParameters
    {
        public int Damage;
        public float AttackCooldown;
        //public float Range;
        public float Multiplier;
        public float AbilityCooldown;
    }
}