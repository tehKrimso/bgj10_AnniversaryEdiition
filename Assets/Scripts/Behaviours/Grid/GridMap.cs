using System;
using System.Collections.Generic;
using Behaviours.Enemies;
using Infrastructure;
using UnityEngine;

namespace Behaviours.Grid
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnerController _enemySpawnerController;
        
        private TileBase[,] _tiles;

        private List<TileBase> buildableTiles;
        private TileBase centerTile;

        public void Init()
        {
            _tiles = Bootstrapper.Instance.Services.Resolve<TilesFactory>().BuildGrid();
        }

        public TileBase GetTile(int x, int y)
        {
            return _tiles[x, y];
        }
    }
}
