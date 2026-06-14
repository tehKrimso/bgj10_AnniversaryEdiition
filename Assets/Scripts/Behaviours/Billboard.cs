using UnityEngine;

namespace Behaviours
{
    public class Billboard : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.forward = _camera.transform.forward;
        }
    }
}
