using Components.Interact;
using Controllers.Actor;
using Controllers.Reciep;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.Interact
{
    public interface IInteractStateFactory
    {
        InteractState Create(InteractableItemComponent component);
    }

    public abstract class InteractStateFactory : IInteractStateFactory
    {
        protected IActorController _actorController;

        protected InteractStateFactory(IActorController actorController)
        {
            this._actorController = actorController;
        }

        public virtual InteractState Create(InteractableItemComponent component)
        {
            switch (component.Type)
            {
                case InteractableItemType.TAKE:
                    TakeInteractableItemComponent takeComponent = (component as TakeInteractableItemComponent);
                    return this._TakeInteractState(takeComponent.Item, takeComponent.Amount);
                case InteractableItemType.RECYCLING:
                    return this._RecyclingInteractState((component as RecyclingInteractableItemComponent)?.Reciep);
                default:
                    return this._DefaultInteractState();
            }
        }

        private InteractState _TakeInteractState(SO_Interactable item, int amount)
        {
            return new InteractState()
            {
                OnStart = () => { this._actorController.CharacterController.Jump(2f); },
                OnCancel = () => { },
                OnFinish = () => { },
                OnProcess = (float percent) => { },
                Time = 0,
                TimeToEnd = 1f,
                Percent = 0f,
                Process = false
            };
        }

        private InteractState _RecyclingInteractState(SO_Reciep reciep)
        {
            return new InteractState()
            {
                OnStart = () => { this._actorController.CharacterController.Jump(2f); },
                OnCancel = () => { },
                OnFinish = () => { },
                OnProcess = (float percent) => { },
                Time = 0,
                TimeToEnd = reciep.TimeToFinish,
                Percent = 0f,
                Process = false
            };
        }

        private InteractState _DefaultInteractState()
        {
            return new InteractState()
            {
                OnStart = () => { this._actorController.CharacterController.Jump(2f); },
                OnCancel = () => { },
                OnFinish = () => { },
                OnProcess = (float percent) => { },
                Time = 0,
                TimeToEnd = 0,
                Percent = 0f,
                Process = false
            };
        }
    }
}