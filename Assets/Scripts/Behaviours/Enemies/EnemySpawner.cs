using System.Collections.Generic;
using Behaviours.Grid;
using UnityEngine;

namespace Behaviours.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public bool isOccupide;
        
        public TileBase parentTile;
        public List<TileBase> pathToCenter;

        public void SetActive(bool isActive)
        {
            enabled = isActive;
        }
    }
}