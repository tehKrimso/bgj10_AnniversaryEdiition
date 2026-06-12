using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

namespace Behaviours.Grid
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField]
        private int Width;
        [SerializeField]
        private int Height;

        private TileBase[,] _tiles;

        private List<TileBase> buildableTiles;
        private TileBase centerTile;

        private void Start()
        {
            _tiles = Bootstrapper.Instance.Services.Resolve<TilesFactory>().BuildGrid();
        }

        public TileBase GetTile(int x, int y)
        {
            return _tiles[x, y];
        }
    }
}
