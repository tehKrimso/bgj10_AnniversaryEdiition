using System;
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

        protected float _attackTimer;

        protected void Update()
        {
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= parameters.AttackCooldown)
            {
                PerformBasicAttack();
                _attackTimer = 0f;
            }
        }

        public void SetNewTowerIndicator(bool isActive)
        {
            //show new tower indicator
            //hide on first click
            //throw new NotImplementedException();
        }

        public abstract void PerformBasicAttack();

        public abstract void PerformAbility();
    }

    [Serializable]
    public class TowerParameters
    {
        public float Damage;
        public float AttackCooldown;
        public float Range;
        public float Multiplier;
        public float AbilityCooldown;
    }
}