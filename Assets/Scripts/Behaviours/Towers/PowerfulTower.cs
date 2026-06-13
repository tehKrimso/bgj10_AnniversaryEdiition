using Behaviours.Enemies;
using UnityEngine;

namespace Behaviours
{
    public class PowerfulTower : BaseTower
    {
        public float DoubleTargetTime;
        private BaseEnemy _mainTarget;
        private BaseEnemy _secondaryTarget;
        public override void PerformBasicAttack()
        {
            Debug.Log("PowerfulTowerAttack");
        }

        public override void PerformAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}