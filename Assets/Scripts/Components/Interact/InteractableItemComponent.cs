using System;
using Controllers.Actor;
using Controllers.Interact;
using UnityEngine;

namespace Components.Interact
{
    public abstract class InteractableItemComponent : MonoBehaviour
    {
        protected readonly IInteractableItemController _interactableItemController = new InteractableItemController();

        public IInteractableItemController InteractableItemController => this._interactableItemController;

        private void Update()
        {
            this._interactableItemController.Update(Time.deltaTime);
        }

        public abstract void StartInteract(IActorController actorController, InteractState interactState);
        public abstract void StopInteract(IActorController actorController, InteractState interactState);
    }
}