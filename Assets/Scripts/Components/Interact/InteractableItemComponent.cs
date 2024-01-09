using Controllers.Actor;
using Controllers.Interact;
using ScriptableObjects;
using UnityEngine;

namespace Components.Interact
{
    public enum InteractableItemType
    {
        TAKE, // Взять
        RECYCLING, // Перерабатывать
        USE, // Использовать (ждать)
        OPEN, // Открыть (хранилище)
        PRODUCE, // Производящее здание с меню
    }

    public abstract class InteractableItemComponent : MonoBehaviour
    {
        [SerializeField] protected SO_Interactable _interactableType;
        [SerializeField] protected InteractableItemType _type;

        protected readonly IInteractableItemController _interactableItemController = new InteractableItemController();

        public IInteractableItemController InteractableItemController => this._interactableItemController;
        public InteractableItemType Type => this._type;
        public SO_Interactable InteractableType => this._interactableType;

        private void Update()
        {
            this._interactableItemController.Update(Time.deltaTime);
        }

        public abstract void StartInteract(IActorController actorController, InteractState interactState);
        public abstract void StopInteract(IActorController actorController);
    }
}