using UnityEngine;

namespace Behaviours
{
    public class TowerIndicators : MonoBehaviour
    {
        public GameObject newTowerIndicator;
        public GameObject selectedTowerIndicator;
        public GameObject controlledByPlayerIndicator;
        
        public void SetNewTowerIndicator(bool isActive)
        {
            newTowerIndicator.SetActive(isActive);
        }

        public void SetSelectedTowerIndicator(bool isActive)
        {
            selectedTowerIndicator.SetActive(isActive);
        }
    }
}