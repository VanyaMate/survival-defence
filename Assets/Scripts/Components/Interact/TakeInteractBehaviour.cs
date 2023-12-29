using ScriptableObjects;
using UnityEngine;

namespace Components.Interact
{
    public class TakeInteractBehaviour : InteractBehaviour
    {
        [SerializeField] private SO_InventoryItem _item;
        [SerializeField] private int _amount;

        public override void Interact(PlayerBehaviour playerBehaviour)
        {
            ActorBehaviour actorBehaviour = playerBehaviour.CurrentActor;
            if (actorBehaviour.InventoryController != null)
            {
                actorBehaviour.InventoryController.PutItem(this._item, this._amount);
                Destroy(gameObject);
            }
        }
    
        public override void OnHover(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("TAKE:HOVER" + this._name);
        }

        public override void OnUnHover(PlayerBehaviour playerBehaviour)
        {
            Debug.Log("TAKE:UN_HOVER" + this._name);
        }
    }
}