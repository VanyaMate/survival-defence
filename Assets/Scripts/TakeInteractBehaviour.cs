using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class TakeInteractBehaviour : InteractBehaviour
{
    [SerializeField] private SO_InventoryItem _item;
    [SerializeField] private int _amount;

    public override void Interact(PlayerBehaviour playerBehaviour)
    {
        Debug.Log(
            "TAKE: " +
            this._name +
            " TO INVENTORY OF " +
            this._item.Title
        );
        playerBehaviour.CurrentActor.InventoryController.PutItem(this._item, this._amount);
        Destroy(gameObject);
    }
}