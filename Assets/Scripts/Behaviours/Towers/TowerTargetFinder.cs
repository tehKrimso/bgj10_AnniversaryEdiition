using System.Collections.Generic;
using Behaviours.Enemies;
using UnityEngine;

namespace Behaviours
{
    public class TowerTargetFinder : MonoBehaviour
    {
        [SerializeField]
        private List<BaseEnemy> _potentialTargets;
        
        public List<BaseEnemy> GetPotentialTargets() => _potentialTargets;

        public void RemoveFromPotentialTargets(BaseEnemy enemy)
        {
            if (_potentialTargets.Contains(enemy))
                _potentialTargets.Remove(enemy);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BaseEnemy enemy))
            {
                Debug.Log($"{enemy.gameObject.name} in range");
                _potentialTargets.Add(enemy);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BaseEnemy enemy))
            {
                Debug.Log($"{enemy.gameObject.name} out of range");
                _potentialTargets.Remove(enemy);
            }
        }
    }
}