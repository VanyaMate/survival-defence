using System;
using UnityEngine;

namespace Components
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Awake()
        {
            
        }

        void Update()
        {
            Vector3 cameraPosition = this._camera.transform.position;
            transform.LookAt(cameraPosition);
        }
    }
}