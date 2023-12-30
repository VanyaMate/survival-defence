using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Components.Interact
{
    public class UseHandRecyclingMachineInteractBehaviour : InteractBehaviour
    {
        [SerializeField] private SO_Reciep _reciep;
        private IProcessController _processController = new ProcessController();

        public override InteractBehaviour Interact(PlayerBehaviour playerBehaviour)
        {
            if (this._processController.InProcess())
            {
                return this.ProcessInteract(playerBehaviour);
            }
            else
            {
                IInventoryController<SO_InventoryItem> inventory = playerBehaviour.CurrentActor.InventoryController;
                if (inventory != null)
                {
                    List<string> result = new List<string>();
                    bool haveAll = true;
                    this._reciep.Reciep.From.ForEach(
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
                        this._interactText = $"Делаем {this._reciep.Title}";
                        this._processController.Start(
                            this._reciep.TimeToFinish,
                            (float progress) => Debug.Log($"Progress: {progress}%"),
                            () => { Debug.Log("Cancel"); },
                            () =>
                            {
                                this._reciep.Reciep.From.ForEach(
                                    (item) => { inventory.TakeItem(item.Item, item.Amount); }
                                );
                                this._reciep.Reciep.To.ForEach(
                                    (item) => { inventory.PutItem(item.Item, item.Amount); }
                                );
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

            return this;
        }

        public override InteractBehaviour ProcessInteract(PlayerBehaviour playerBehaviour)
        {
            this._progress = this._processController.Tick(Time.deltaTime);
            return this;
        }

        public override void CancelInteract(PlayerBehaviour playerBehaviour)
        {
            if (this._processController.InProcess())
            {
                this._processController.Stop();
            }
        }

        public override void OnHover(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("USE:HOVER" + this._name);
        }

        public override void OnUnHover(PlayerBehaviour playerBehaviour)
        {
            this._processController.Stop();
            Debug.Log("USE:UN_HOVER" + this._name);
        }
    }
}