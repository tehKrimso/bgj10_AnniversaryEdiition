using System.Collections.Generic;
using System.Linq;
using Behaviours;
using Behaviours.Grid;
using UnityEngine;
using Random = System.Random;

namespace Infrastructure
{
    public class TilesFactory : IService
    {
        private readonly TilePrefabsSettings _tilePrefabs;
        private readonly MapTileLayoutSettings _layoutSettings;
        
        private Random _random;

        public TilesFactory(TilePrefabsSettings tilePrefabs, MapTileLayoutSettings layoutSettings)
        {
            _tilePrefabs = tilePrefabs;
            _layoutSettings = layoutSettings;
            
            _random = new Random();
        }

        public TileBase[,] BuildGrid(out TileBase centerTile, out List<TileBase> buildableTiles)
        {
            TileBase[,] grid = new TileBase[_layoutSettings.Width, _layoutSettings.Height];
            
            buildableTiles = new List<TileBase>();
            centerTile = null;
            
            
            for (int x = 0; x < _layoutSettings.Width; x++)
            {
                for (int y = 0; y < _layoutSettings.Height; y++)
                {
                    var tileType = _layoutSettings.GetTile(x, y);
                    
                    var prefab = _tilePrefabs.prefabsForTiles.FirstOrDefault(t => t.tileType == tileType)?.prefabs;
                    
                    GameObject tile = GameObject.Instantiate(prefab[_random.Next(prefab.Length)]);
                    tile.transform.position = new Vector3(x * _layoutSettings.TileSize + _layoutSettings.TilesOffset, 0, y * _layoutSettings.TileSize + _layoutSettings.TilesOffset);
                    var tileComponent = tile.GetComponent<TileBase>();
                    tileComponent.SetGridPos(x,y);
                    grid[x, y] = tileComponent;
                    
                    if(tileType == TileType.Center)
                        centerTile = tileComponent;
                    
                    if(tileType == TileType.Ground)
                    {
                        buildableTiles.Add(tileComponent);
                    }
                }
            }
            
            return grid;
        }
    }
}