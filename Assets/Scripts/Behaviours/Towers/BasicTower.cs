using System;
using System.Collections.Generic;
using System.Linq;
using Behaviours.Enemies;
using UnityEngine;

namespace Behaviours
{
    public class BasicTower : BaseTower
    {
        public int AdditionalAttackSpeedOnControl;
        public int AoeAbilityDamage;
        private BaseEnemy _target;

        
        
        private void Update()
        {
            base.Update();
        }


        public override void PerformBasicAttack()
        {
            if (_target == null)
            {
                var targets = targetFinder.GetPotentialTargets();
                _target = targets.OrderBy(t => Vector3.Distance(t.transform.position, _centerTilePosition)).FirstOrDefault();
                
            }
            
            _target?.TakeDamage(parameters.Damage + _additionalDamageOnControl);
            
            Debug.Log("BasicTowerAttack");
        }

        public override void PerformAbility()
        {
            foreach (BaseEnemy enemy in targetFinder.GetPotentialTargets())
            {
                enemy.TakeDamage(AoeAbilityDamage);
            }
            
            StartAbilityCooldownTimer();
        }

        
    }
}