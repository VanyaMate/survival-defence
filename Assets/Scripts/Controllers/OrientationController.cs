using System;
using UnityEngine;

namespace Controllers
{
    public interface IOrientationController
    {
        void Rotate(Vector2 direction);
    }

    public class OrientationController : IOrientationController
    {
        private readonly Transform _verticalTransform;
        private readonly Transform _horizontalTransform;

        private float _verticalOrientation;
        private float _horizontalOrientation;

        public OrientationController(Transform vertical, Transform horizontal)
        {
            this._verticalTransform = vertical;
            this._horizontalTransform = horizontal;

            this._verticalOrientation = this._verticalTransform.rotation.eulerAngles.x;
            this._horizontalOrientation = this._horizontalTransform.rotation.eulerAngles.y;
        }

        public void Rotate(Vector2 direction)
        {
            this._verticalOrientation -= direction.y;
            this._horizontalOrientation += direction.x;

            this._verticalOrientation = Math.Clamp(this._verticalOrientation, -90f, 90f);

            this._verticalTransform.rotation = Quaternion.Euler(
                this._verticalOrientation,
                this._horizontalOrientation,
                0
            );
            this._horizontalTransform.rotation = Quaternion.Euler(
                0,
                this._horizontalOrientation,
                0
            );
        }
    }
}