using Controllers.Character;
using Controllers.Inventory;
using Controllers.Interact;
using ScriptableObjects;

namespace Controllers.Actor
{
    public interface IActorController
    {
        ICharacterController CharacterController { get; }
        IInventoryController<SO_InventoryItem> InventoryController { get; }
        IInteractController InteractController { get; }
    }

    public class ActorController : IActorController
    {
        private readonly ICharacterController _characterController;
        private readonly IInventoryController<SO_InventoryItem> _inventoryController;
        private readonly IInteractController _interactController;

        public ActorController(
            ICharacterController characterController,
            IInventoryController<SO_InventoryItem> inventoryController,
            IInteractController interactController
        )
        {
            this._characterController = characterController;
            this._inventoryController = inventoryController;
            this._interactController = interactController;
        }

        public ICharacterController CharacterController => this._characterController;
        public IInventoryController<SO_InventoryItem> InventoryController => this._inventoryController;
        public IInteractController InteractController => this._interactController;
    }
}