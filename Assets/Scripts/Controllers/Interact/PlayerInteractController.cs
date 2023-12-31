using Components.Interact;
using UnityEngine;

namespace Controllers.Interact
{
    public interface IPlayerInteractController : IUpdateController
    {
        public InteractableItemComponent Item { get; }
        public void SetDistance(float distance);
        public void Enable(bool state);
    }

    public class PlayerInteractController : IPlayerInteractController
    {
        private InteractableItemComponent _item;
        private UnityEngine.Camera _camera;
        private float _distance;

        public InteractableItemComponent Item => this._item;

        public PlayerInteractController(UnityEngine.Camera camera, float distance)
        {
            this._camera = camera;
            this._distance = distance;
        }

        private void Raycast(float distance)
        {
            Ray ray = this._camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                InteractableItemComponent interactBehaviour = hit.collider.GetComponent<InteractableItemComponent>();
                if (interactBehaviour != null)
                {
                    if (interactBehaviour != this._item)
                    {
                        this._item = interactBehaviour;
                    }

                    return;
                }
            }

            this._item = null;
        }

        public void Update(float deltaTime)
        {
            this.Raycast(this._distance);
        }

        public void SetDistance(float distance)
        {
            this._distance = distance;
        }

        public void Enable(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !state;
        }
    }
}