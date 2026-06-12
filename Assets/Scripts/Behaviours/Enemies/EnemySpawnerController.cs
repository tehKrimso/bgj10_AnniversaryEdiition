using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using UnityEngine;

namespace Behaviours.Enemies
{
    public class EnemySpawnerController : MonoBehaviour
    {
        private List<EnemySpawner> _spawners;
        private EnemiesFactory _enemiesFactory;
        public void Init()
        {
            _spawners = FindObjectsOfType<EnemySpawner>().ToList();
            _enemiesFactory = Bootstrapper.Instance.Services.Resolve<EnemiesFactory>();
        }
    }
}