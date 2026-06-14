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
        public TowerIndicators towerIndicators;

        public SphereCollider targetTrigger;

        protected GameLoopController _gameLoopController;
        protected float _attackTimer;
        protected bool _isActive;
        protected Vector3 _centerTilePosition;

        protected float _abilityCooldownTimer;
        protected bool _abilityOnCooldown;


        protected bool _isControlledByPlayer;
        protected bool _isMovementReady = true;
        protected float _movementCooldownTimer;

        protected int _additionalDamageOnControl;

        protected void Start()
        {
            towerIndicators = GetComponentInChildren<TowerIndicators>();
            _gameLoopController = Bootstrapper.Instance.Services.Resolve<GameLoopController>();
            _centerTilePosition = _gameLoopController.GetCenterTile().gameObject.transform.position;
            targetTrigger.radius = parameters.TargetRadius;
        }

        protected void Update()
        {
            if(!_isActive)
                return;
            
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= parameters.AttackCooldown - (_isControlledByPlayer ? parameters.AttackCooldownReductionOnControl : 0f))
            {
                PerformBasicAttack();
                _attackTimer = 0f;
            }

            //check if movmeent ready
            if (!_isMovementReady)
            {
                _movementCooldownTimer -= Time.deltaTime;
                if (_movementCooldownTimer <= 0f)
                {
                    _movementCooldownTimer = 0f;
                    _isMovementReady = true;
                }
            }
            
            //check if ability ready
            if (_abilityOnCooldown)
            {
                CheckAbilityCooldown();
            }
        }

        public bool ReadyToMove() => _isMovementReady;
        
        public void StartMovementCooldown()
        {
            _isMovementReady = false;
            _movementCooldownTimer = parameters.MovementCooldown;
        }
        
        public bool IsControlledByPlayer() => _isControlledByPlayer;
        
        public void SetControlledByPlayer(bool isControlledByPlayer)
        {
            _isControlledByPlayer = isControlledByPlayer;
            towerIndicators.controlledByPlayerIndicator.SetActive(isControlledByPlayer);

            if (isControlledByPlayer) //when take control
            {
                targetTrigger.radius = parameters.TargetRadius + parameters.AditionalRadiusOnControl;
                _additionalDamageOnControl = parameters.AdditionalDamageOnControl;
            }
            else //when release control
            {
                targetTrigger.radius = parameters.TargetRadius;
                _additionalDamageOnControl = 0;
            }
        }

        public void SetActive(bool active) => _isActive = active;

        public void SetNewTowerIndicator(bool isActive)
        {
            if(towerIndicators == null)
                towerIndicators = GetComponentInChildren<TowerIndicators>();
            
            towerIndicators.newTowerIndicator.SetActive(isActive);
        }

        public void SetSelectedTowerIndicator(bool isActive)
        {
            towerIndicators.selectedTowerIndicator.SetActive(isActive);
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

        public bool AbilityOnCooldown()
        {
            return _abilityOnCooldown;
        }
    }

    [Serializable]
    public class TowerParameters
    {
        [Header("Damage")]
        public int Damage;
        public int AdditionalDamageOnControl;
        
        [Header("TargetRadius")]
        public float TargetRadius;
        public float AditionalRadiusOnControl;
        
        [Header("Cooldowns")]
        public float AttackCooldown;
        public float AttackCooldownReductionOnControl;
        public float AbilityCooldown;
        public float MovementCooldown;
    }
}