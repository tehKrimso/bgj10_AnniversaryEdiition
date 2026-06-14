using System.Linq;
using Behaviours.Enemies;
using UnityEngine;

namespace Behaviours
{
    public class PowerfulTower : BaseTower
    {
        public float DoubleTargetTime;
        private BaseEnemy _mainTarget;
        private BaseEnemy _secondaryTarget;

        private bool _doubleTargetOn;
        private float _doubleTargetTimer;
        
        
        private void Update()
        {
            base.Update();
            if(_doubleTargetOn)
                CheckDoubleTarget();
        }
        
        public override void PerformBasicAttack()
        {
            //perf?
            if (_mainTarget == null || (_secondaryTarget == null && _doubleTargetOn))
            {
                var targets = targetFinder.GetPotentialTargets();
                if (targets.Count <= 0)
                    return;
                var targetsByDistanceToCore = targets.OrderBy(t => Vector3.Distance(t.transform.position, _centerTilePosition)).ToList();
                _mainTarget = targetsByDistanceToCore[0];
                
                if(!_doubleTargetOn)
                    return;
                
                _secondaryTarget = targets.Count > 1 ? targetsByDistanceToCore[1] : targetsByDistanceToCore[0];
            }
            
            _mainTarget?.TakeDamage(parameters.Damage + _additionalDamageOnControl);
            if(_doubleTargetOn)
                _secondaryTarget?.TakeDamage(parameters.Damage + _additionalDamageOnControl);
            
            Debug.Log("PowerfulTowerAttack");
        }

        public override void PerformAbility()
        {
            _doubleTargetTimer = DoubleTargetTime;
            _doubleTargetOn = true;
            
            StartAbilityCooldownTimer();
        }

        private void CheckDoubleTarget()
        {
            _doubleTargetTimer -= Time.deltaTime;
            if (_doubleTargetTimer <= 0)
            {
                _doubleTargetOn = false;
            }
        }
    }
}