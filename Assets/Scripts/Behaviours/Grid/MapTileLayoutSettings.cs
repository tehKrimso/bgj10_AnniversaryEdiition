using System;
using UnityEngine;

namespace Behaviours.Grid
{
    [CreateAssetMenu(menuName = "Grid/MapTileLayoutSettings", fileName = "MapTileLayoutSettings")]
    public class MapTileLayoutSettings: ScriptableObject
    {
        public int Width;
        public int Height;
        public float TileSize;
        public float TilesOffset = 0.05f;
        
        public TileType[] Tiles;

        public void Resize()
        {
            Array.Resize(ref Tiles, Width * Height);
        }
        
        public int GetIndex(int x, int y)
        {
            return y * Width + x;
        }

        public TileType GetTile(int x, int y)
        {
            return Tiles[GetIndex(x, y)];
        }
        
    }
    
}