using System;
using UnityEngine;

namespace Infrastructure
{
    public class PlayerInputService : IService
    {
        private readonly Camera _camera;

        public PlayerInputService()
        {
            _camera = Camera.main;
        }

        public bool LeftCLick()
        {
            return Input.GetKeyDown(KeyCode.Mouse0);
        }

        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
        
        public Vector2 GetMousePositionOnScreen()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public bool ControlButtonUp()
        {
            return Input.GetKeyUp(KeyCode.Q);
        }

        public bool AbilityButtonUp()
        {
            return Input.GetKeyUp(KeyCode.E);
        }

        public bool MoveButtonUp()
        {
            return Input.GetKeyUp(KeyCode.M);
        }

        public bool CancelButtonDown()
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }
}