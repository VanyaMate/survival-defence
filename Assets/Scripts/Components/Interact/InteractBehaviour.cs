using UnityEngine;

namespace Components.Interact
{
    public abstract class InteractBehaviour : MonoBehaviour
    {
        [SerializeField] protected string _name;
        [SerializeField] protected string _preInteractText;

        public abstract void Interact(PlayerBehaviour playerBehaviour);
        public abstract void OnHover(PlayerBehaviour playerBehaviour);
        public abstract void OnUnHover(PlayerBehaviour playerBehaviour);
    }
}