using System;
using UnityEngine;

namespace Behaviours.Grid
{
    [CreateAssetMenu(menuName = "Grid/TilePrefabsSettings", fileName = "TilePrefabsSettings")]
    public class TilePrefabsSettings : ScriptableObject
    {
        public PrefabForTileType[] prefabsForTiles;
    }

    [Serializable]
    public class PrefabForTileType
    {
        public TileType tileType;
        public GameObject prefab;
    }
}