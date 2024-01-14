using System.Collections.Generic;
using Controllers.Actor;
using Controllers.Interact;
using ScriptableObjects;
using UnityEngine;

namespace Components.Interact
{
    public class ProduceInteractableItemComponent : InteractableItemComponent
    {
        [SerializeField] public SO_Produce Produce;

        public override void StartInteract(IActorController actorController, InteractState interactState)
        {
            this._interactableItemController.StartInteract(
                actorController.InteractController,
                new InteractState()
                {
                    OnCancel = interactState.OnCancel,
                    OnFinish = interactState.OnFinish,
                    OnProcess = interactState.OnProcess,
                    OnStart = interactState.OnStart,
                    Time = interactState.Time,
                    TimeToEnd = interactState.TimeToEnd,
                    Percent = interactState.Percent,
                    Process = interactState.Process
                }
            );
        }

        public override void StopInteract(IActorController actorController)
        {
            this._interactableItemController.StopInteract(actorController.InteractController);
        }
    }
}