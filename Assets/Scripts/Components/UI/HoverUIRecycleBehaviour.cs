using Controllers.Actor;
using Controllers.Inventory;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Controllers.UI
{
    public class HoverUIRecycleBehaviour : UIElementBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Transform _reciepItemsContainer;
        [SerializeField] private HoverUIRecycleItemBehaviour _recycleItemPrefab;

        private SO_Reciep _reciep;
        private IActorController _actorController;

        public void Set(SO_Reciep reciep, IActorController actorController)
        {
            if (this._reciep != reciep || actorController != this._actorController)
            {
                if (this._actorController != null)
                {
                    this._actorController.InventoryController.Unsubscribe(OnInventoryChangeHandler);
                }

                this._reciep = reciep;
                this._actorController = actorController;
                this._actorController.InventoryController.Subscribe(OnInventoryChangeHandler);
                this._RenderReciepItems();
            }

            this._title.text = reciep.Title;
        }

        private void OnInventoryChangeHandler(Inventory<SO_InventoryItem> inventory)
        {
            this._RenderReciepItems();
        }

        private void _RenderReciepItems()
        {
            for (int i = 0; i < this._reciepItemsContainer.childCount; i++)
            {
                Destroy(this._reciepItemsContainer.GetChild(i).gameObject);
            }

            this._reciep.Reciep.From.ForEach(
                (item) =>
                {
                    HoverUIRecycleItemBehaviour recycleItem = Instantiate(
                        this._recycleItemPrefab,
                        this._reciepItemsContainer
                    );
                    recycleItem.Set(
                        item.Item,
                        item.Amount,
                        this._actorController.InventoryController.GetItemAmount(item.Item)
                    );
                    recycleItem.Show();
                }
            );
        }
    }
}