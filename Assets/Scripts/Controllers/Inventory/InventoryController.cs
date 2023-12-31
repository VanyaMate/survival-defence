using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Utils;

namespace Controllers.Inventory
{
    public class Inventory<T> : Dictionary<T, int>
    {
    }

    public interface IInventoryController<TItem> : IEmitter<Inventory<TItem>>
    {
        Inventory<TItem> Inventory { get; }
        float Weight { get; }
        float MaxWeight { get; }

        Inventory<TItem> PutItem(TItem item, int amount);
        bool TakeItem(TItem item, int amount);
        int TakeAllItems(TItem item);
        bool CheckItemAmount(TItem item, int amount);
        int GetItemAmount(TItem item);
    }

    public class InventoryController : Emitter<Inventory<SO_InventoryItem>>, IInventoryController<SO_InventoryItem>
    {
        private readonly Inventory<SO_InventoryItem> _inventory;
        private readonly SO_Inventory _inventoryType;

        public InventoryController(SO_Inventory inventoryType, Inventory<SO_InventoryItem> inventory)
        {
            this._inventory = inventory;
            this._inventoryType = inventoryType;
        }

        public Inventory<SO_InventoryItem> Inventory => this._inventory;
        public float Weight => this._inventory.Aggregate(0f, (acc, item) => acc + (item.Key.Weight * item.Value));
        public float MaxWeight => this._inventoryType.Weight;

        public Inventory<SO_InventoryItem> PutItem(SO_InventoryItem item, int amount)
        {
            if (this._inventory.TryGetValue(item, out int inventoryAmount))
            {
                this._inventory[item] = inventoryAmount + amount;
            }
            else
            {
                this._inventory.Add(item, amount);
            }

            this.Invoke(this._inventory);
            return this._inventory;
        }

        public bool TakeItem(SO_InventoryItem item, int amount)
        {
            if (this._inventory.TryGetValue(item, out int inventoryAmount))
            {
                if (inventoryAmount > amount)
                {
                    this._inventory[item] = inventoryAmount - amount;
                    this.Invoke(this._inventory);
                    return true;
                }
                else if (inventoryAmount == amount)
                {
                    this._inventory.Remove(item);
                    this.Invoke(this._inventory);
                    return true;
                }
            }

            return false;
        }

        public int TakeAllItems(SO_InventoryItem item)
        {
            if (this._inventory.Remove(item, out int inventoryAmount))
            {
                this.Invoke(this._inventory);
                return inventoryAmount;
            }

            return 0;
        }

        public bool CheckItemAmount(SO_InventoryItem item, int amount)
        {
            if (this._inventory.TryGetValue(item, out int inventoryAmount))
            {
                return inventoryAmount >= amount;
            }

            return false;
        }

        public int GetItemAmount(SO_InventoryItem item)
        {
            if (this._inventory.TryGetValue(item, out int inventoryAmount))
            {
                return inventoryAmount;
            }

            return 0;
        }
    }
}