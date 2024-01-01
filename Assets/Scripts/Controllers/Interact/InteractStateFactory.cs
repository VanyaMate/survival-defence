using Components.Interact;
using Controllers.Actor;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.Interact
{
    public interface IInteractStateFactory
    {
        InteractState Create(InteractableItemType type, SO_Item item);
    }

    public abstract class InteractStateFactory : IInteractStateFactory
    {
        protected IActorController _actorController;

        protected InteractStateFactory(IActorController actorController)
        {
            this._actorController = actorController;
        }

        public virtual InteractState Create(InteractableItemType type, SO_Item item)
        {
            switch (type)
            {
                case InteractableItemType.TAKE:
                    return this._TakeInteractState(item);
                default:
                    return this._DefaultInteractState(item);
            }
        }

        private InteractState _TakeInteractState(SO_Item item)
        {
            return new InteractState()
            {
                OnStart = () => { this._actorController.CharacterController.Jump(10f); },
                OnCancel = () => { },
                OnFinish = () => { },
                OnProcess = (float percent) => { },
                Time = 0,
                TimeToEnd = 1f,
                Percent = 0f
            };
        }

        private InteractState _DefaultInteractState(SO_Item item)
        {
            return new InteractState()
            {
                OnStart = () => { },
                OnCancel = () => { },
                OnFinish = () => { },
                OnProcess = (float percent) => { },
                Time = 0,
                TimeToEnd = 0,
                Percent = 0f,
                Process = true
            };
        }
    }
}