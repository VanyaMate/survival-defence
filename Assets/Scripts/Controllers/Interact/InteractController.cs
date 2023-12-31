namespace Controllers.Interact
{
    public interface IInteractController
    {
        public float Interact(InteractableItemController interactableItemController, InteractState interactState);
        public void Stop(InteractableItemController interactableItemController);
    }

    public class InteractController : IInteractController
    {
        private bool _interacted;
        private float _percent;

        public float Interact(InteractableItemController interactableItemController, InteractState interactState)
        {
            if (this._interacted)
            {
                return this._percent;
            }
            else
            {
                interactableItemController.StartInteract(
                    this,
                    new InteractState()
                    {
                        OnFinish = () =>
                        {
                            this._interacted = false;
                            interactState.OnFinish();
                        },
                        OnCancel = () =>
                        {
                            this._interacted = false;
                            interactState.OnCancel();
                        },
                        OnProcess = (float percent) =>
                        {
                            this._percent = percent;
                            interactState.OnProcess(this._percent);
                        },
                        Time = interactState.Time,
                        TimeToEnd = interactState.TimeToEnd,
                    }
                );

                this._interacted = true;
                this._percent = 0;

                return this._percent;
            }
        }

        public void Stop(InteractableItemController interactableItemController)
        {
            interactableItemController.StopInteract(this);
        }
    }
}