using System;
using UnityEngine;

namespace Temp
{
    [RequireComponent(typeof(ActorBehaviour))]
    public class MoveForward : MonoBehaviour
    {
        private ActorBehaviour _actorBehaviour;

        private void Awake()
        {
            this._actorBehaviour = this.GetComponent<ActorBehaviour>();
        }

        private void Update()
        {
            this._actorBehaviour.ActorController.CharacterController.MoveDirection(Vector2.up);
            this._actorBehaviour.ActorController.CharacterController.Rotate(Vector2.right * .2f);
        }
    }
}