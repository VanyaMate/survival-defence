using System;
using UnityEngine;

namespace Controllers
{
    interface IFirstPersonCameraController
    {
        void Rotate(Vector2 direction);
    }

    public class FirstPersonCameraController : IFirstPersonCameraController
    {
        private readonly Transform _actorWithCamera;
        private readonly UnityEngine.Camera _firstPersonCamera;

        private float _x;
        private float _y;

        public FirstPersonCameraController(Transform actor, UnityEngine.Camera camera)
        {
            this._actorWithCamera = actor;
            this._firstPersonCamera = camera;

            this._x = this._firstPersonCamera.transform.rotation.x;
            this._y = this._actorWithCamera.rotation.y;
        }


        public void Rotate(Vector2 direction)
        {
            this._x -= direction.x;
            this._y += direction.y;
            this._x = Math.Clamp(this._x, -90f, 90f);
            this._actorWithCamera.rotation = Quaternion.Euler(0, this._y, 0);
            this._firstPersonCamera.transform.rotation = Quaternion.Euler(this._x, this._y, 0);
        }
    }
}