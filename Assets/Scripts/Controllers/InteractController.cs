using UnityEngine;

namespace Controllers
{
    public interface IInteractController
    {
        void Raycast(float distance);
        void Interact(PlayerBehaviour player);
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
                if (interactBehaviour != null)
                {
                    this._interactItem = interactBehaviour;
                }
                else
                {
                    this._interactItem = null;
                }
            }
        }

        public void Interact(PlayerBehaviour player)
        {
            if (this._interactItem != null)
            {
                this._interactItem.Interact(player);
            }
        }
    }
}