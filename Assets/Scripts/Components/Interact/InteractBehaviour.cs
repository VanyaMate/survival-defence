using System;
using Controllers;
using UnityEngine;

namespace Components.Interact
{
    public abstract class InteractBehaviour : MonoBehaviour
    {
        [SerializeField] protected string _name;
        [SerializeField] protected string _preInteractText;
        protected float _progress = 0;
        protected string _interactText = "";
        protected IProcessController _processController;

        private void Awake()
        {
            this._processController = new ProcessController();
        }

        public float Progress => this._progress;
        public string InteractText => this._interactText;

        public abstract InteractBehaviour Interact(PlayerBehaviour playerBehaviour);

        public virtual InteractBehaviour ProcessInteract(PlayerBehaviour playerBehaviour)
        {
            this._progress = this._processController.Tick(Time.deltaTime);
            return this;
        }

        public abstract void CancelInteract(PlayerBehaviour playerBehaviour);
        public abstract void OnHover(PlayerBehaviour playerBehaviour);
        public abstract void OnUnHover(PlayerBehaviour playerBehaviour);
    }
}