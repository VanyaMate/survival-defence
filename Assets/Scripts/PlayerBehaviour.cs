using System;
using Controllers.Input;
using Controllers.Interact;
using UI.Progress;
using UnityEngine;
using IPlayerInteractController = Controllers.Interact.IPlayerInteractController;
using PlayerInteractController = Controllers.Interact.PlayerInteractController;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ActorBehaviour _actor;
    [SerializeField] private UIProgress _uiProgress;

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
        this._interactController = new PlayerInteractController(this._camera, 3f);
        this._interactController.Enable(true);
    }

    private void Start()
    {
        this._interactStateFactory = new PlayerInteractStateFactory(this._actor.ActorController, this._uiProgress);
    }

    private void Update()
    {
        this._interactController.Update(Time.deltaTime);
        this._actor.ActorController.CharacterController.MoveDirection(this._inputController.Direction() * 2f);
        this._actor.ActorController.CharacterController.Rotate(this._inputController.MouseMove());

        if (this._inputController.Jump())
        {
            this._actor.ActorController.CharacterController.Jump(10);
        }

        if (this._inputController.Use() && this._interactController.Item != null)
        {
            this._interactController.Item.StartInteract(
                this._actor.ActorController,
                this._interactStateFactory.Create(
                    this._interactController.Item.Type,
                    this._interactController.Item.Item
                )
            );
        }
    }
}