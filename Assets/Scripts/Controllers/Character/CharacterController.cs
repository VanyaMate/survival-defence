using UnityEngine;

namespace Controllers.Character
{
    public interface ICharacterController : IUpdateController
    {
        void MoveDirection(Vector2 direction);
        void Jump(float power);
        void Rotate(Vector2 direction);
        void DisableMove(bool state);
        void DisableRotate(bool state);
        void DisableJump(bool state);
    }

    public class UnityCharacterController : ICharacterController
    {
        private readonly CharacterController _unityCharacterController;
        private readonly IOrientationController _orientationController;
        private readonly float _gravity;
        private Vector3 _moveDirection = Vector3.zero;
        private bool _disableMove = false;
        private bool _disableRotate = false;
        private bool _disableJump = false;

        public UnityCharacterController(
            CharacterController unityCharacterController,
            IOrientationController orientationController,
            float gravity
        )
        {
            this._unityCharacterController = unityCharacterController;
            this._orientationController = orientationController;
            this._gravity = gravity;
        }

        public void Update(float deltaTime)
        {
            if (!this._unityCharacterController.isGrounded || this._moveDirection.y > 0)
            {
                this._moveDirection.y -= this._gravity * 2 * deltaTime;
            }
            else
            {
                this._moveDirection.y = -1f;
            }

            this._unityCharacterController.Move(this._moveDirection * deltaTime);
        }

        public void MoveDirection(Vector2 direction)
        {
            if (this._disableMove)
            {
                return;
            }

            this._moveDirection = this._unityCharacterController.transform.forward * direction.y +
                                  this._unityCharacterController.transform.right * direction.x +
                                  new Vector3(0, this._moveDirection.y, 0);
        }

        public void Jump(float power)
        {
            if (this._disableJump)
            {
                return;
            }

            if (this._unityCharacterController.isGrounded)
            {
                this._moveDirection.y = power;
            }
        }

        public void Rotate(Vector2 direction)
        {
            if (this._disableRotate)
            {
                return;
            }

            this._orientationController.Rotate(direction);
        }

        public void DisableMove(bool state)
        {
            this._disableMove = state;
        }

        public void DisableRotate(bool state)
        {
            this._disableRotate = state;
        }

        public void DisableJump(bool state)
        {
            this._disableJump = state;
        }
    }
}