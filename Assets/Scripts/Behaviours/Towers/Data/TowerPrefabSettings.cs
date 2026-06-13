using System;
using UnityEngine;

namespace Behaviours.Data
{
    [CreateAssetMenu(menuName = "FightCycle/TowerPrefabSettings", fileName = "TowerPrefabSettings")]
    public class TowerPrefabSettings : ScriptableObject
    {
        public PrefabForTowerType[] towerPrefabs;
    }
    
    [Serializable]
    public class PrefabForTowerType
    {
        public TowerType towerType;
        public GameObject prefab;
    }
}