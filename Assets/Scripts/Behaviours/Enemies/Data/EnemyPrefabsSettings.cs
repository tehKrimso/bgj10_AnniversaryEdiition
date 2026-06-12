using System;
using UnityEngine;

namespace Behaviours.Enemies
{
    [CreateAssetMenu(menuName = "FightCycle/EnemyPrefabsSettings", fileName = "EnemyPrefabsSettings")]
    public class EnemyPrefabsSettings : ScriptableObject
    {
        public PrefabForEnemyType[] enemyPrefabs;
    }
    
    [Serializable]
    public class PrefabForEnemyType
    {
        public EnemyType enemyType;
        public GameObject prefab;
    }
}