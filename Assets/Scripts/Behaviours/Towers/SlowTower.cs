using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Behaviours
{
    public class SlowTower : BaseTower
    {
        public float SlowRate = 0.4f;
        public float AdditionalSlowRateOnControl = 0.2f;
        public float SlowTime = 1f;
        public float FreezeTime = 1f;

        private void Update()
        {
            base.Update();
        }
        public override void PerformBasicAttack()
        {
            var actualSlowRate = SlowRate + (_isControlledByPlayer ? AdditionalSlowRateOnControl: 0);
            
            foreach (var target in targetFinder.GetPotentialTargets())
            {
                
                target.ApplySlow(actualSlowRate, SlowTime);
            }
        }

        public override void PerformAbility()
        {
            foreach (var target in targetFinder.GetPotentialTargets())
            {
                target.ApplyFreeze(FreezeTime);
            }
            
            StartAbilityCooldownTimer();
        }
    }
}