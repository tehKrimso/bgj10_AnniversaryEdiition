using System.Collections.Generic;
using Behaviours.Grid;
using UnityEngine;

namespace Behaviours.Enemies
{
    public class BaseEnemy : MonoBehaviour
    {
        private List<TileBase> _waypoints;

        //give waypoints on spawn
        //move between waypoints
        public void SetPath(List<TileBase> spawnerPathToCenter)
        {
            _waypoints = spawnerPathToCenter;
        }
    }
}
