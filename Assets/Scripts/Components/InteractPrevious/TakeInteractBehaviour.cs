using Controllers;
using ScriptableObjects;
using UnityEngine;

namespace Components.InteractPrevious
{
    public class TakeInteractBehaviour : InteractBehaviour
    {
        [SerializeField] private SO_InventoryItem _item;
        [SerializeField] private int _amount;
        private IProcessController _processController = new ProcessController();

        public override InteractBehaviour Interact(PlayerBehaviour playerBehaviour)
        {
            if (this._processController.InProcess())
            {
                this._progress = this._processController.Tick(Time.deltaTime);
            }
            else
            {
                ActorBehaviour actorBehaviour = playerBehaviour.CurrentActor;
                if (actorBehaviour.ActorController.InventoryController != null)
                {
                    this._interactText = $"Берем {this._item.Title}";
                    this._processController.Start(
                        1,
                        onCancelCallback: () => { Debug.Log("Take cancel"); },
                        onProgressCallback: (float progress) => { Debug.Log("Take progress " + progress + "%"); },
                        onSuccessCallback: () =>
                        {
                            actorBehaviour.ActorController.InventoryController.PutItem(this._item, this._amount);
                            Destroy(gameObject);
                        }
                    );
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
            Debug.Log("TAKE:HOVER" + this._name);
        }

        public override void OnUnHover(PlayerBehaviour playerBehaviour)
        {
            this._processController.Stop();
            Debug.Log("TAKE:UN_HOVER" + this._name);
        }
    }
}