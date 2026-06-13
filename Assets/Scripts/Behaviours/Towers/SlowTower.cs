using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Behaviours
{
    public class SlowTower : BaseTower
    {
        public float SlowRate = 0.4f;
        public float FreezeTime = 1f;

        private void Update()
        {
            base.Update();
        }
        public override void PerformBasicAttack()
        {
            Debug.Log("SlowTowerAttack");
        }

        public override void PerformAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}