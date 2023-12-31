using System.Collections.Generic;
using Controllers;
using Controllers.Inventory;
using ScriptableObjects;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace UI.Inventory
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem _itemPrefab;
        [SerializeField] private GameObject _container;
        private bool _opened = false;
        private IInventoryController<SO_InventoryItem> _inventoryController;

        public void Show(bool state)
        {
            this.gameObject.SetActive(state);
            if (state)
            {
                this._inventoryController?.Subscribe(this.Render);
            }
            else
            {
                this._inventoryController?.Unsubscribe(this.Render);
            }
        }

        public void Set(IInventoryController<SO_InventoryItem> inventoryController)
        {
            this._inventoryController?.Unsubscribe(this.Render);
            this._inventoryController = inventoryController;
            this._inventoryController.Subscribe(this.Render);
            this.Render(this._inventoryController.Inventory);
        }

        private void Render(Dictionary<SO_InventoryItem, int> inventory)
        {
            this._Clear();
            foreach (KeyValuePair<SO_InventoryItem, int> inventoryItem in inventory)
            {
                UIInventoryItem item = Instantiate(this._itemPrefab, this._container.transform);
                item.Set(inventoryItem.Key, inventoryItem.Value, 100);
            }
        }

        private void _Clear()
        {
            for (int i = 0; i < this._container.transform.childCount; i++)
            {
                Destroy(this._container.transform.GetChild(i).gameObject);
            }
        }
    }
}