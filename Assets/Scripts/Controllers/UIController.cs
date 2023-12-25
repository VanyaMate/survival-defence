using UnityEngine;

namespace Controllers
{
    public interface IUIController
    {
        bool Opened();
        void Inventory(bool open);
        void Menu(bool open);
        void CloseAll();
    }

    public class UIController : IUIController
    {
        private GameObject _inventory;
        private GameObject _menu;
        private bool _opened = false;

        public UIController(GameObject inventory, GameObject menu)
        {
            this._inventory = inventory;
            this._menu = menu;
        }

        public bool Opened()
        {
            return this._opened;
        }

        public void Inventory(bool open)
        {
            this._Open(this._inventory, open);
        }

        public void Menu(bool open)
        {
            this._Open(this._menu, open);
        }

        public void CloseAll()
        {
            this._CloseAll();
        }

        private void _CursorState(bool state)
        {
            Cursor.visible = state;
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        }

        private void _CloseAll()
        {
            this._inventory.SetActive(false);
            this._menu.SetActive(false);
            this._CursorState(false);
            this._opened = false;
        }

        private void _Open(GameObject menu, bool state)
        {
            this._CloseAll();
            this._CursorState(state);
            menu.SetActive(state);
            this._opened = state;
        }
    }
}