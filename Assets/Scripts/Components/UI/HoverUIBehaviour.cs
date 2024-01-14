using Components.Interact;
using Controllers.Actor;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.UI
{
    public class HoverUIBehaviour : UIElementBehaviour
    {
        [SerializeField] private MonoBehaviour _use;
        [SerializeField] private HoverUITakeBehaviour _take;
        [SerializeField] private HoverUIRecycleBehaviour _recycle;

        public void ShowUse()
        {
            this.HideAll();
            this._use.gameObject.SetActive(true);
        }

        public void ShowTake(TakeInteractableItemComponent item)
        {
            this.HideAll();
            this._take.Set(item.Item, item.Amount);
            this._take.Show();
        }

        public void ShowRecycle(RecyclingInteractableItemComponent item, IActorController actorController)
        {
            this.HideAll();
            this._recycle.Set(item.Reciep, actorController);
            this._recycle.Show();
        }

        public void ShowProduce(ProduceInteractableItemComponent item)
        {
            this.HideAll();
            this._use.gameObject.SetActive(true);
        }

        public void HideAll()
        {
            this._use.gameObject.SetActive(false);
            this._take.Hide();
            this._recycle.Hide();
        }
    }
}