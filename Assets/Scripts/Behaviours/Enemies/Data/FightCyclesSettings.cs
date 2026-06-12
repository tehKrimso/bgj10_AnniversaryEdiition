using System;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours.Enemies
{
    [CreateAssetMenu(menuName = "FightCycle/FightCyclesSettings", fileName = "FightCyclesSettings")]
    public class FightCyclesSettings : ScriptableObject
    {
        public List<FightCycle> fightCycles;
    }

    [Serializable]
    public class FightCycle
    {
        public int activeRoadsCount;
        public float delayBetweenPackSpawns;
        public List<EnemyPack> enemyPacks;
    }

    [Serializable]
    public class EnemyPack
    {
        public EnemyType enemyType;
        public int enemyCount;
        public float delayBetweenSingleEnemySpawn;
    }
}