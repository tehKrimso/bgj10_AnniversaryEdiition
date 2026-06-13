using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Behaviours
{
    public class SlowTower : BaseTower
    {
        public float SlowRate = 0.4f;
        public float SlowTime = 1f;
        public float FreezeTime = 1f;

        private void Update()
        {
            base.Update();
        }
        public override void PerformBasicAttack()
        {
            foreach (var target in targetFinder.GetPotentialTargets())
            {
                target.ApplySlow(SlowRate, SlowTime);
            }
        }

        public override void PerformAbility()
        {
            foreach (var target in targetFinder.GetPotentialTargets())
            {
                target.ApplyFreeze(FreezeTime);
            }
        }
    }
}