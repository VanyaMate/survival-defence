using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using UnityEngine;

namespace Components.Interact
{
    public class UseRecyclingMachineInteractBehaviour : InteractBehaviour
    {
        [SerializeField] private Reciep _reciep;

        public override void Interact(PlayerBehaviour playerBehaviour)
        {
            IInventoryController<SO_InventoryItem> inventory = playerBehaviour.CurrentActor.InventoryController;
            if (inventory != null)
            {
                List<string> result = new List<string>();
                bool haveAll = true;
                this._reciep.From.ForEach(
                    (item) =>
                    {
                        int amount = inventory.CheckItem(item.Item);
                        result.Add($"{item.Item.name} {amount}/{item.Amount}");
                        if (amount < item.Amount)
                        {
                            haveAll = false;
                        }
                    }
                );

                if (haveAll)
                {
                    this._reciep.From.ForEach(
                        (item) =>
                        {
                            inventory.TakeItem(item.Item, item.Amount);
                        }
                    );
                    this._reciep.To.ForEach(
                        (item) =>
                        {
                            inventory.PutItem(item.Item, item.Amount);
                        }    
                    );
                }
                else
                {
                    result.ForEach(
                        (row) => { Debug.Log(row); }
                    );
                }
            }
        }

        public override void OnHover(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("USE:HOVER" + this._name);
        }

        public override void OnUnHover(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("USE:UN_HOVER" + this._name);
        }
    }
}