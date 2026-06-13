using System.Collections.Generic;
using Behaviours.Grid;
using UnityEngine;

namespace Behaviours.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public TileBase parentTile;
        public List<TileBase> pathToCenter;
    }
}