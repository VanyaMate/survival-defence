using System;
using UnityEngine;

namespace Components
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Update()
        {
            this._camera.transform.position = PlayerBehaviour.instance.CurrentActor.CameraPosition.transform.position;
            this._camera.transform.rotation = PlayerBehaviour.instance.CurrentActor.CameraPosition.transform.rotation;
        }
    }
}