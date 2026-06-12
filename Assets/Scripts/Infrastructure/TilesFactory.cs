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

        public TileBase[,] BuildGrid()
        {
            TileBase[,] grid = new TileBase[_layoutSettings.Width, _layoutSettings.Height];

            
            
            for (int x = 0; x < _layoutSettings.Width; x++)
            {
                for (int y = 0; y < _layoutSettings.Height; y++)
                {
                    var tileType = _layoutSettings.GetTile(x, y);
                    
                    var prefab = _tilePrefabs.prefabsForTiles.FirstOrDefault(t => t.tileType == tileType)?.prefab;
                    
                    GameObject tile = GameObject.Instantiate(prefab);
                    tile.transform.position = new Vector3(x * _layoutSettings.TileSize + _layoutSettings.TilesOffset, 0, y * _layoutSettings.TileSize + _layoutSettings.TilesOffset);
                    var tileComponent = tile.GetComponent<TileBase>();
                    tileComponent.SetGridPos(x,y);
                    grid[x, y] = tileComponent;
                }
            }
            
            return grid;
        }
    }
}