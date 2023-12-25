using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public interface IInventoryItemController<TItem>
    {
        // Вернуть тип текущего айтема
        TItem GetItem();

        // Добавить какое то количество в текущее
        // Возвращается количество которое НЕ ПОМЕСТИЛОСЬ
        int PutAmount(int amount);

        // Взять какое то количетсво из текущего
        // Возвращается количество которое ВЗЯТО
        int TakeAmount(int amount);
    }

    public class ScriptableObjectInventoryItemController : IInventoryItemController<SO_InventoryItem>
    {
        private int _amount;
        private SO_InventoryItem _item;

        public ScriptableObjectInventoryItemController(SO_InventoryItem itemType, int amount = 0)
        {
            this._item = itemType;
            this._amount = amount;
        }

        public SO_InventoryItem GetItem()
        {
            return this._item;
        }

        public int PutAmount(int amount)
        {
            this._amount += amount;
            return 0;
        }

        public int TakeAmount(int amount)
        {
            if (this._amount < amount)
            {
                int cachedAmount = this._amount;
                this._amount = 0;
                return cachedAmount;
            }
            else
            {
                this._amount -= amount;
                return amount;
            }
        }
    }
}