using System;
using Behaviours.Enemies;
using UnityEngine;

namespace Behaviours
{
    public class BasicTower : BaseTower
    {
        private BaseEnemy _target;
        
        private void Update()
        {
            base.Update();
        }


        public override void PerformBasicAttack()
        {
            
            Debug.Log("BasicTowerAttack");
        }

        public override void PerformAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}