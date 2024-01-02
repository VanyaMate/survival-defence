using Controllers.Actor;
using Controllers.Interact;
using Utils;

namespace Components.Interact
{
    public class UseInteractableItemComponent : InteractableItemComponent
    {
        public IEmitter<bool> Emitter = new Emitter<bool>();

        public override void StartInteract(IActorController actorController, InteractState interactState)
        {
            if (!this._interactableItemController.Blocked)
            {
                this._interactableItemController.StartInteract(
                    actorController.InteractController,
                    new InteractState()
                    {
                        OnStart = interactState.OnStart,
                        OnFinish = () =>
                        {
                            this.Emitter.Invoke(true);
                            interactState.OnFinish();
                        },
                        OnCancel = interactState.OnCancel,
                        OnProcess = interactState.OnProcess,
                        TimeToEnd = interactState.TimeToEnd,
                        Time = interactState.Time,
                        Percent = interactState.Percent,
                        Process = interactState.Process,
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