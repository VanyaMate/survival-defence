using System;
using System.Collections.Generic;
using ScriptableObjects;
using Utils;

namespace Controllers
{
    using Inventory = Dictionary<SO_InventoryItem, int>;

    public interface IInventoryController<TItem>
    {
        // Положить в инвентарь
        // Возвращает количество айтемов которые НЕ ПОМЕСТИЛИСЬ
        int PutItem(TItem item, int amount);

        // Взять из инвентаря
        // Возвращает количество айтемов которые ВЗЯТЫ
        int TakeItem(TItem item, int amount);

        // Проверить наличие в инвентаре нужное количество вещей
        int CheckItem(TItem item);

        // Взять все айтемы из инвентаря
        // Возвращает количество взятых айтемов
        int TakeAllItems(TItem item);

        // Вернуть инвентарь
        Dictionary<TItem, int> ShowInventory();

        // Подписаться на изменения инвентаря
        void Subscribe(EmitterCallback<Inventory> callback);

        // Отписаться от изменений
        void Unsubscribe(EmitterCallback<Inventory> callback);
    }


    public class WeightInventoryController : IInventoryController<SO_InventoryItem>
    {
        private Inventory _inventory;
        private Emitter<Inventory> _emitter = new Emitter<Inventory>();

        public WeightInventoryController(Inventory inventory)
        {
            this._inventory = inventory;
        }

        public int PutItem(SO_InventoryItem item, int amount)
        {
            if (this._inventory.TryGetValue(item, out int inventoryAmount))
            {
                this._inventory[item] = amount + inventoryAmount;
            }
            else
            {
                this._inventory.Add(item, amount);
            }

            this._emitter.Invoke(this._inventory);
            return 0;
        }

        public int TakeItem(SO_InventoryItem item, int amount)
        {
            if (this._inventory.TryGetValue(item, out int inventoryAmount))
            {
                if (amount >= inventoryAmount)
                {
                    this._inventory.Remove(item);
                    this._emitter.Invoke(this._inventory);
                    return inventoryAmount;
                }
                else
                {
                    this._inventory[item] = inventoryAmount - amount;
                    this._emitter.Invoke(this._inventory);
                    return amount;
                }
            }
            else
            {
                return 0;
            }
        }

        public int CheckItem(SO_InventoryItem item)
        {
            this._inventory.TryGetValue(item, out int inventoryAmount);
            return inventoryAmount;
        }

        public int TakeAllItems(SO_InventoryItem item)
        {
            this._inventory.TryGetValue(item, out int inventoryAmount);
            this._inventory.Remove(item);
            return inventoryAmount;
        }

        public Inventory ShowInventory()
        {
            return this._inventory;
        }

        public void Subscribe(EmitterCallback<Inventory> callback)
        {
            this._emitter.Subscribe(callback);
        }

        public void Unsubscribe(EmitterCallback<Inventory> callback)
        {
            this._emitter.Unsubscribe(callback);
        }
    }
}