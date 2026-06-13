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
        
        
        private void Update()
        {
            base.Update();
        }
        
        public override void PerformBasicAttack()
        {
            if (_mainTarget == null)
            {
                var targets = targetFinder.GetPotentialTargets();
                _mainTarget = targets.OrderBy(t => Vector3.Distance(t.transform.position, _centerTilePosition)).FirstOrDefault();
                
            }
            
            _mainTarget?.TakeDamage(parameters.Damage);
            
            Debug.Log("PowerfulTowerAttack");
        }

        public override void PerformAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}