using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<TileBase> _buildableTiles;
        private TileBase _centerTile;
        
        //pathfinding
        private int _width;
        private int _height;
        private Queue<TileBase> _frontier;
        private Dictionary<TileBase,TileBase> _cameFrom;

        public void Init()
        {
            _tiles = Bootstrapper.Instance.Services.Resolve<TilesFactory>().
                BuildGrid(out _centerTile, out _buildableTiles);
            
            _width = _tiles.GetLength(0);
            _height = _tiles.GetLength(1);
            
            _frontier = new Queue<TileBase>();
            _frontier.Enqueue(_centerTile);
            _cameFrom = new Dictionary<TileBase, TileBase>();
            
            InvestigateGraph();
        }

        public TileBase GetTile(int x, int y)
        {
            return _tiles[x, y];
        }

        public List<TileBase> GetPathFromTileToCenter(TileBase tile)
        {
            var path = new List<TileBase>();
            path.Add(tile);
            do
            {
                var nextTile = _cameFrom[path.Last()];
                if (nextTile == null)
                    break;
                path.Add(nextTile);
            }
            while(!path.Contains(_centerTile));
            
            return path;
        }

        private List<TileBase> GetNeighborTiles(TileBase tile)
        {
            List<TileBase> neighbors = new List<TileBase>();
            var tilePos = tile.GetGridPos();

            var left = tilePos.x - 1;
            var right = tilePos.x + 1;
            var top = tilePos.y - 1;
            var bottom = tilePos.y + 1;

            if (left >= 0 && left < _width)
            {
                neighbors.Add(_tiles[left,tilePos.y]);
            }
                

            if (right >= 0 && right < _width)
                neighbors.Add(_tiles[right,tilePos.y]);
            
            if(top >= 0 && top < _height)
                neighbors.Add(_tiles[tilePos.x,top]);
            
            if(bottom >= 0 && bottom < _height)
                neighbors.Add(_tiles[tilePos.x,bottom]);
            
            return neighbors
                .Where(t => t.tileType == TileType.Road || t.tileType == TileType.EnemySpawner)
                .ToList();
        }

        private void InvestigateGraph()
        {
            while (_frontier.Count > 0)
            {
                var currentTile = _frontier.Dequeue();
                var neighbors = GetNeighborTiles(currentTile);
                foreach (var neighborTile in neighbors)
                {
                    if (!_cameFrom.ContainsKey(neighborTile))
                    {
                        _frontier.Enqueue(neighborTile);
                        _cameFrom[neighborTile] = currentTile;
                    }
                }
            }
        }
    }
}
