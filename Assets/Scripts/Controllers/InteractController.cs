using Components.Interact;
using UnityEngine;

namespace Controllers
{
    public delegate InteractBehaviour OnHoverCallback();

    public delegate InteractBehaviour OnUnHoverCallback();

    public interface IInteractController
    {
        void Raycast(float distance);
        InteractBehaviour Interact(PlayerBehaviour player);
        void CancelInteract(PlayerBehaviour playerBehaviour);
    }

    public class InteractController : IInteractController
    {
        private PlayerBehaviour _player;
        private Camera _camera;
        private InteractBehaviour _interactItem;


        public InteractController(Camera camera)
        {
            this._camera = camera;
        }

        public void Raycast(float distance)
        {
            Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                InteractBehaviour interactBehaviour = hit.collider.GetComponent<InteractBehaviour>();

                if ((interactBehaviour == null || this._interactItem != interactBehaviour) && this._interactItem)
                {
                    this._interactItem.OnUnHover(this._player);
                }

                if (interactBehaviour != null)
                {
                    if (interactBehaviour != this._interactItem)
                    {
                        this._interactItem = interactBehaviour;
                        this._interactItem.OnHover(this._player);
                    }
                }
                else
                {
                    this._interactItem = null;
                }
            }
            else
            {
                if (this._interactItem)
                {
                    this._interactItem.OnUnHover(this._player);
                    this._interactItem = null;
                }
            }
        }

        public InteractBehaviour Interact(PlayerBehaviour player)
        {
            if (this._interactItem != null)
            {
                return this._interactItem.Interact(player);
            }

            return null;
        }

        public void CancelInteract(PlayerBehaviour player)
        {
            if (this._interactItem != null)
            {
                this._interactItem.CancelInteract(player);
            }
        }
    }
}