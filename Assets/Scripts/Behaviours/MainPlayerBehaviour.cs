using System;
using System.Collections.Generic;
using Behaviours.Grid;
using Infrastructure;
using UnityEngine;

namespace Behaviours
{
    public class MainPlayerBehaviour : MonoBehaviour
    {
        public LayerMask ClickCollisionMask;
        private GameLoopController _gameLoopController;
        private PlayerInputService _input;
        private BaseTower _selectedTower;
        private BaseTower _controlledTower;
        private Camera _camera;
        
        private List<TileBase> _buildableTiles;

        private bool _isMovingTower;

        private void Start()
        {
            _camera = Camera.main;
            _gameLoopController = Bootstrapper.Instance.Services.Resolve<GameLoopController>();
            _input = Bootstrapper.Instance.Services.Resolve<PlayerInputService>();
        }

        private void Update()
        {
            
            if (_input.LeftCLick())
            {
                if (_selectedTower != null)
                {
                    if (_selectedTower.IsControlledByPlayer() && _isMovingTower)
                    {
                        TryCheckBuildableTile();
                        return;
                    }
                    
                    TrySelectTower();
                    return;
                }
                
                TrySelectTower();
            }

            if (_input.ControlButtonUp())
            {
                TakeControl();
            }

            if (_input.AbilityButtonUp() && 
                _controlledTower != null &&
                _controlledTower.IsControlledByPlayer() &&
                !_controlledTower.AbilityOnCooldown()
                )
            {
                _controlledTower.PerformAbility();
            }

            //add flag?
            if (_input.MoveButtonUp() && 
                _controlledTower != null &&
                _controlledTower.IsControlledByPlayer() &&
                _controlledTower.ReadyToMove()
                )
            {
                ShowBuildableTiles();
                _isMovingTower = true;
            }

            if (_input.CancelButtonDown())
            {
                if(_buildableTiles != null)
                    DeselectBuildableTiles();

                _isMovingTower = false;
            }
            
        }

        private void ShowBuildableTiles()
        {
            _buildableTiles = _gameLoopController.GetFreeTilesToBuild();
        }

        private void DeselectBuildableTiles()
        {
            foreach (var tile in _buildableTiles)
            {
                tile.SetBuildableIndicator(false);
            }
        }

        private void TakeControl()
        {
            if (_selectedTower != null && !_selectedTower.IsControlledByPlayer())
            {
                _controlledTower?.SetControlledByPlayer(false);
                _controlledTower = _selectedTower;
                _controlledTower.SetControlledByPlayer(true);
            }
        }

        private void SelectTower(BaseTower tower)
        {
            ReleaseTower();
            
            _selectedTower = tower;
            _selectedTower.SetSelectedTowerIndicator(true);
        }
        
        private void ReleaseTower()
        {
            _selectedTower?.SetSelectedTowerIndicator(false);
            _selectedTower = null;
        }

        private void UpdateTowerPosition()
        {
            if(_selectedTower == null) return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane dragPlane = new Plane(Vector3.up, new Vector3(0, _selectedTower.transform.position.y, 0));

            if (!dragPlane.Raycast(ray, out float distance))
                return;

            Vector3 worldPoint = ray.GetPoint(distance);
            worldPoint.y = _selectedTower.transform.position.y; // Гарантируем фиксированную Y

            _selectedTower.transform.position = worldPoint;
        }
        
        private void TrySelectTower()
        {
            Ray ray =  _camera.ScreenPointToRay(_input.GetMousePosition());
            
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ClickCollisionMask))
                return;
            
            if(!hit.collider.TryGetComponent(out BaseTower tower))
                return;
            
            ReleaseTower();

            tower.SetNewTowerIndicator(false);
            SelectTower(tower);
                
        }
        
        private void TryCheckBuildableTile()
        {
            Ray ray =  _camera.ScreenPointToRay(_input.GetMousePosition());

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ClickCollisionMask))
                return;
            
            if(!hit.collider.TryGetComponent(out TileBase tile))
                return;

            tile.SetPlacedTower(_selectedTower);
            DeselectBuildableTiles();
            
            _isMovingTower = false;

            if (_gameLoopController.IsCycleRunning())
            {
                _selectedTower.StartMovementCooldown();
            }
        }
    }
}