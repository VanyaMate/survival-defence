using UnityEngine;

namespace Components.Interact
{
    public abstract class InteractBehaviour : MonoBehaviour
    {
        [SerializeField] protected string _name;
        [SerializeField] protected string _preInteractText;
        protected float _progress = 0;
        protected string _interactText = "";
        public float Progress => this._progress;
        public string InteractText => this._interactText;

        public abstract InteractBehaviour Interact(PlayerBehaviour playerBehaviour);
        public abstract InteractBehaviour ProcessInteract(PlayerBehaviour playerBehaviour);
        public abstract void CancelInteract(PlayerBehaviour playerBehaviour);
        public abstract void OnHover(PlayerBehaviour playerBehaviour);
        public abstract void OnUnHover(PlayerBehaviour playerBehaviour);
    }
}