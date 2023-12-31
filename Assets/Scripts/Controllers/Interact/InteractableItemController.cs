using System.Collections.Generic;
using Controllers.Actor;
using Utils;

namespace Controllers.Interact
{
    public delegate void OnInteractProcessCallback(float percent);

    public delegate void OnInteractCancelCallback();

    public delegate void OnInteractFinishCallback();

    public delegate void OnInteractStartCallback();

    public class InteractState : Emitter<float>
    {
        public OnInteractCancelCallback OnCancel;
        public OnInteractFinishCallback OnFinish;
        public OnInteractProcessCallback OnProcess;
        public OnInteractStartCallback OnStart;
        public float Percent = 0;
        public float Time;
        public float TimeToEnd;
    }

    public class Interactors : Dictionary<IInteractController, InteractState>
    {
    }

    public interface IInteractableItemController : IUpdateController, IEmitter<Interactors>
    {
        Interactors Interactors { get; }
        void StartInteract(IInteractController actorController, InteractState state, bool force = false);
        void StopInteract(IInteractController actorController);
        void StopAll();
    }

    public class InteractableItemController : Emitter<Interactors>, IInteractableItemController
    {
        private Interactors _interactControllers = new Interactors();

        public Interactors Interactors => this._interactControllers;

        public void StartInteract(IInteractController interactController, InteractState state, bool force = false)
        {
            if (force)
            {
                this.StopInteract(interactController);
            }

            if (!this._interactControllers.TryGetValue(interactController, out InteractState previousState))
            {
                this._interactControllers.Add(interactController, state);
                this.Invoke(this._interactControllers);
            }
        }

        public void StopInteract(IInteractController interactController)
        {
            if (this._interactControllers.Remove(interactController, out InteractState previousState))
            {
                previousState.OnCancel();
            }

            this.Invoke(this._interactControllers);
        }

        public void StopAll()
        {
            foreach (var (interactController, interactState) in this._interactControllers)
            {
                interactState.OnCancel();
                this._interactControllers.Remove(interactController);
            }

            this.Invoke(this._interactControllers);
        }

        public void Update(float deltaTime)
        {
            List<IInteractController> finished = new List<IInteractController>();

            foreach (var (interactController, interactState) in this._interactControllers)
            {
                InteractState controller = this._interactControllers[interactController];
                controller.Time += deltaTime;
                controller.Percent = 100 / controller.TimeToEnd * controller.Time;
                controller.OnProcess(controller.Percent);
                controller.Invoke(controller.Percent);
                if (controller.Time >= controller.TimeToEnd)
                {
                    controller.OnFinish();
                    finished.Add(interactController);
                }
            }

            finished.ForEach((item) => this._interactControllers.Remove(item));
        }
    }
}