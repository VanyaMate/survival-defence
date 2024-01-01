using Controllers.Actor;
using Controllers.Interact;
using ScriptableObjects;
using UnityEngine;

namespace Components.Interact
{
    public class TakeInteractableItemComponent : InteractableItemComponent
    {
        [SerializeField] private int _amount;

        public override void StartInteract(IActorController actorController, InteractState interactState)
        {
            this._interactableItemController.StartInteract(
                actorController.InteractController,
                new InteractState()
                {
                    OnCancel = interactState.OnCancel,
                    OnProcess = interactState.OnProcess,
                    OnStart = interactState.OnStart,
                    OnFinish = () =>
                    {
                        actorController.InventoryController.PutItem(this._item, this._amount);
                        Destroy(gameObject);
                        interactState.OnFinish();
                    },
                    Time = 0,
                    TimeToEnd = interactState.TimeToEnd,
                    Percent = 0
                }
            );
        }

        public override void StopInteract(IActorController actorController, InteractState interactState)
        {
            this._interactableItemController.StopInteract(actorController.InteractController);
        }
    }
}