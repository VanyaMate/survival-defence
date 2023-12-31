using Controllers.Character;
using Controllers.Inventory;
using ScriptableObjects;

namespace Controllers.Actor
{
    public interface IActorController
    {
        ICharacterController CharacterController { get; }
        IInventoryController<SO_InventoryItem> InventoryController { get; }
    }

    public class ActorController : IActorController
    {
        private readonly ICharacterController _characterController;
        private readonly IInventoryController<SO_InventoryItem> _inventoryController;

        public ActorController(
            ICharacterController characterController,
            IInventoryController<SO_InventoryItem> inventoryController
        )
        {
            this._characterController = characterController;
            this._inventoryController = inventoryController;
        }

        public ICharacterController CharacterController => this._characterController;
        public IInventoryController<SO_InventoryItem> InventoryController => this._inventoryController;
    }
}