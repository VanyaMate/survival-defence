using Controllers.Actor;
using Controllers.Interact;
using ScriptableObjects;
using UnityEngine;

namespace Components.Interact
{
    public class TakeInteractableItemComponent : InteractableItemComponent
    {
        [SerializeField] public SO_Interactable Item;
        [SerializeField] public int Amount;

        public override void StartInteract(IActorController actorController, InteractState interactState)
        {
            if (!this._interactableItemController.Blocked)
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
                            Destroy(gameObject);
                            interactState.OnFinish();
                            actorController.InventoryController.PutItem(this.Item, this.Amount);
                            this._interactableItemController.StopAll();
                            this._interactableItemController.BlockForNewInteractors(true);
                        },
                        Time = interactState.Time,
                        TimeToEnd = interactState.TimeToEnd,
                        Percent = interactState.Percent,
                        Process = interactState.Process
                    }
                );
            }
        }

        public override void StopInteract(IActorController actorController)
        {
            this._interactableItemController.StopInteract(actorController.InteractController);
        }
    }
}