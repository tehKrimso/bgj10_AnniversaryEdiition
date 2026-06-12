using System;
using Infrastructure;
using UnityEngine;

namespace Behaviours
{
    public class MainPlayerBehaviour : MonoBehaviour
    {
        private PlayerInputService _input;
        private BaseTower _tower;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _input = Bootstrapper.Instance.Services.Resolve<PlayerInputService>();
        }

        private void Update()
        {
            if (_input.LeftCLick())
            {
                if (_tower != null)
                {
                    ReleaseTower();
                    return;
                }
                
                TryGetObjectByClick();
            }
            
            UpdateTowerPosition();
        }

        private void GrabTower(BaseTower tower)
        {
            _tower = tower;
        }
        
        private void ReleaseTower() => _tower = null;

        private void UpdateTowerPosition()
        {
            if(_tower == null) return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane dragPlane = new Plane(Vector3.up, new Vector3(0, _tower.transform.position.y, 0));

            if (!dragPlane.Raycast(ray, out float distance))
                return;

            Vector3 worldPoint = ray.GetPoint(distance);
            worldPoint.y = _tower.transform.position.y; // Гарантируем фиксированную Y

            _tower.transform.position = worldPoint;
        }
        
        private void TryGetObjectByClick()
        {
            Ray ray =  _camera.ScreenPointToRay(_input.GetMousePosition());

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                return;
            
            if(!hit.collider.TryGetComponent(out BaseTower tower))
                return;
            
            GrabTower(tower);
                
        }
    }
}