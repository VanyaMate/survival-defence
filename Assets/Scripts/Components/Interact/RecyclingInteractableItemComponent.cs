using Controllers.Actor;
using Controllers.Interact;
using Controllers.Inventory;
using Controllers.Reciep;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Components.Interact
{
    public class RecyclingInteractableItemComponent : InteractableItemComponent
    {
        [SerializeField] public SO_Reciep Reciep;

        public override void StartInteract(IActorController actorController, InteractState interactState)
        {
            if (!this._interactableItemController.Blocked)
            {
                if (ReciepWorker.Check(actorController.InventoryController, this.Reciep.Reciep).Valid)
                {
                    EmitterCallback<Inventory<SO_InventoryItem>> handler =
                        this._OnInventoryChangeHandler(actorController);

                    this._interactableItemController.StartInteract(
                        actorController.InteractController,
                        new InteractState()
                        {
                            OnCancel = () =>
                            {
                                actorController.InventoryController.Unsubscribe(handler);
                                interactState.OnCancel();
                            },
                            OnProcess = interactState.OnProcess,
                            OnStart = () =>
                            {
                                actorController.InventoryController.Subscribe(handler);
                                interactState.OnStart();
                            },
                            OnFinish = () =>
                            {
                                actorController.InventoryController.Unsubscribe(handler);
                                interactState.OnFinish();
                                ReciepWorker.Convert(actorController.InventoryController, this.Reciep.Reciep);
                            },
                            Time = interactState.Time,
                            TimeToEnd = interactState.TimeToEnd,
                            Percent = interactState.Percent,
                            Process = interactState.Process
                        }
                    );
                }
            }
        }

        public override void StopInteract(IActorController actorController)
        {
            this._interactableItemController.StopInteract(actorController.InteractController);
        }

        private EmitterCallback<Inventory<SO_InventoryItem>> _OnInventoryChangeHandler(IActorController actorController)
        {
            return (Inventory<SO_InventoryItem> inv) =>
            {
                this._interactableItemController.StopInteract(actorController.InteractController);
            };
        }
    }
}