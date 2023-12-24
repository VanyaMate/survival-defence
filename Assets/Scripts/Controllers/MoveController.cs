using System;
using UnityEngine;

namespace Controllers
{
    public interface IMoveController
    {
        void ChangePosition();
        void MoveDirection(Vector2 direction);
        void Jump(float power);
    }

    public class MoveController : IMoveController
    {
        private readonly ActorBehaviour _actor;
        private readonly Transform _actorTransform;
        private readonly Rigidbody _actorRigidBody;

        public MoveController(ActorBehaviour actor)
        {
            this._actor = actor;
            this._actorTransform = this._actor.transform;
            this._actorRigidBody = this._actor.GetComponent<Rigidbody>();
        }

        public void ChangePosition()
        {
        }

        public void MoveDirection(Vector2 direction)
        {
            if (this._actorRigidBody)
            {
                this._actorRigidBody.velocity = this._actorTransform.forward * direction.y +
                                                this._actorTransform.right * direction.x;
            }
        }

        public void Jump(float power)
        {
            if (this._actorRigidBody)
            {
                this._actorRigidBody.velocity += new Vector3(0, power, 0);
            }
        }
    }

    public class MoveCharacterController : IMoveController
    {
        private readonly CharacterController _characterController;
        private readonly float _gravity = 9.81f;
        private Vector3 _moveDirection = Vector3.zero;

        public MoveCharacterController(CharacterController controller)
        {
            this._characterController = controller;
        }

        public void ChangePosition()
        {
            if (!this._characterController.isGrounded || this._moveDirection.y > 0)
            {
                this._moveDirection.y -= this._gravity * 2 * Time.deltaTime;
            }
            else
            {
                this._moveDirection.y = -1f;
            }

            this._characterController.Move(this._moveDirection * Time.deltaTime);
        }

        public void MoveDirection(Vector2 direction)
        {
            this._moveDirection = this._characterController.transform.forward * direction.y +
                                  this._characterController.transform.right * direction.x +
                                  new Vector3(0, this._moveDirection.y, 0);
        }

        public void Jump(float power)
        {
            if (this._characterController.isGrounded)
            {
                this._moveDirection.y = power;
            }
        }
    }
}