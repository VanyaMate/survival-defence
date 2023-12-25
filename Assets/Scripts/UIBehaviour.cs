using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using UI.Inventory;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private UIInventory _inventory;
    [SerializeField] private GameObject _menu;

    private IUIController _uiController;

    private void Awake()
    {
        this._uiController = new UIController(
            this._inventory.gameObject,
            this._menu
        );
    }

    public bool Opened()
    {
        return this._uiController.Opened();
    }

    public void Inventory(bool state, IInventoryController<SO_InventoryItem> inventory)
    {
        this._inventory.Set(inventory);
        this._uiController.Inventory(state);
    }

    public void Menu(bool state)
    {
        this._uiController.Menu(state);
    }

    public void CloseAll()
    {
        this._uiController.CloseAll();
    }
}