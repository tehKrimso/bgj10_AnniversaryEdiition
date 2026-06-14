using UnityEngine;
using UnityEngine.Serialization;

namespace Behaviours.Grid
{
    public class TileBase : MonoBehaviour
    {
        public GameObject ReadyToBuildIndicator;
        public Transform PointToMoveTo;
        public Transform towerPlacementTransform;
        public TileType tileType;
        public bool buildAllowed;
        public bool IsOccupied => _placedTower != null;
        public GameObject TeleportationIndicator;

        private Vector2Int _gridPosition;
        private BaseTower _placedTower;

        public void SetGridPos(int x, int y)
        {
            _gridPosition = new Vector2Int(x, y);
        }

        public void ResetPlacedTower()
        {
            _placedTower = null;
        }

        public void SetPlacedTower(BaseTower tower)
        {
            tower.parentTile?.ResetPlacedTower();
            tower.parentTile = null;
            _placedTower = tower;
            _placedTower.transform.SetParent(towerPlacementTransform);
            _placedTower.transform.localPosition = Vector3.zero;
            _placedTower.parentTile = this;
        }
        
        public Vector2Int GetGridPos() => _gridPosition;

        public void SetBuildableIndicator(bool activeState)
        {
            ReadyToBuildIndicator.SetActive(activeState);
        }
    }


  
}