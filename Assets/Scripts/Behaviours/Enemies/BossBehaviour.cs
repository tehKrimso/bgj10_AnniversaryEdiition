using System.Collections;
using UnityEngine;

namespace Behaviours.Enemies
{
    public class BossBehaviour : BaseEnemy
    {
        public float BossTeleportCooldown = 2f;
        public float BossTeleportDuration = 2f;
        
        private float _teleportCooldown;
        private bool _isTeleporting;

        protected void Start()
        {
            base.Start();
            _teleportCooldown = BossTeleportCooldown;
        }

        protected void Update()
        {
            base.Update();

            if (_teleportCooldown > 0)
            {
                _teleportCooldown -= Time.deltaTime;
            }
            else
            {
                if(!_isTeleporting)
                    StartCoroutine(PerformTeleport());
            }
        }

        private IEnumerator PerformTeleport()
        {
            _shouldMove = false;
            _isTeleporting = true;
            var newPath = _gameLoopController.GetNewPath(_waypoints[0]);

            var currentTilesToCenter = _waypoints.Count - 1 - _currentWaypointIndex;
            var newPathIndex = newPath.Count - 1 - currentTilesToCenter;
            if(newPathIndex < 0)
                newPathIndex = 0;
            var destinationTile = newPath[newPathIndex];
            destinationTile.TeleportationIndicator.SetActive(true);
            
            
            yield return new WaitForSeconds(BossTeleportDuration);
            
            //teleport
            destinationTile.TeleportationIndicator.SetActive(false);
            transform.position = destinationTile.towerPlacementTransform.position;
            _waypoints = newPath;
            _currentWaypointIndex = newPathIndex;
            _teleportCooldown = BossTeleportCooldown;
            _isTeleporting = false;
            _shouldMove = true;
        }
    }
}