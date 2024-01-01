using System;
using Controllers.Input;
using Controllers.Interact;
using UI.Inventory;
using UI.Progress;
using Unity.VisualScripting;
using UnityEngine;
using IPlayerInteractController = Controllers.Interact.IPlayerInteractController;
using PlayerInteractController = Controllers.Interact.PlayerInteractController;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ActorBehaviour _actor;
    [SerializeField] private UIProgress _uiProgress;
    [SerializeField] private UIInventory _uiInventory;

    [Header("Sens")] [SerializeField] [Range(100, 600)]
    private float _x_sens;

    [SerializeField] [Range(100, 600)] private float _y_sens;

    private IInputController _inputController;
    private IPlayerInteractController _interactController;
    private IInteractStateFactory _interactStateFactory;

    public ActorBehaviour CurrentActor => this._actor;

    private void Awake()
    {
        this._inputController = new InputController();
    }

    private void Start()
    {
        this._interactStateFactory = new PlayerInteractStateFactory(this._actor.ActorController, this._uiProgress);
        this._interactController = new PlayerInteractController(this._camera, this._actor.ActorController, 3f);
        this._interactController.Enable(true);
        this._uiInventory.Set(this._actor.ActorController.InventoryController);
    }

    private void Update()
    {
        this._interactController.Update(Time.deltaTime);
        this._actor.ActorController.CharacterController.MoveDirection(this._inputController.Direction() * 2f);
        this._actor.ActorController.CharacterController.Rotate(this._inputController.MouseMove());

        if (this._inputController.Jump())
        {
            this._actor.ActorController.CharacterController.Jump(5);
        }

        if (this._inputController.Use() && this._interactController.Item != null)
        {
            this._interactController.Item.StartInteract(
                this._actor.ActorController,
                this._interactStateFactory.Create(this._interactController.Item)
            );
        }

        if (this._inputController.Close())
        {
            if (
                this._interactController.Item != null &&
                this._interactController.Item.InteractableItemController.Interactors.TryGetValue(
                    this._actor.ActorController.InteractController,
                    out InteractState state
                )
            )
            {
                this._interactController.Item.StopInteract(this._actor.ActorController);
            }
            else if (this._uiInventory.gameObject.activeSelf)
            {
                this._uiInventory.Show(false);
                this._interactController.Enable(true);
                this._actor.ActorController.CharacterController.DisableRotate(false);
            }
        }

        if (this._inputController.OpenInventory())
        {
            bool active = this._uiInventory.gameObject.activeSelf;
            this._actor.ActorController.CharacterController.DisableRotate(!active);
            this._interactController.Enable(active);
            this._uiInventory.Show(!active);
        }
    }
}