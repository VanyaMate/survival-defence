using Controllers.Input;
using Controllers.Interact;
using UnityEngine;
using IPlayerInteractController = Controllers.Interact.IPlayerInteractController;
using PlayerInteractController = Controllers.Interact.PlayerInteractController;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ActorBehaviour _actor;

    [Header("Sens")] [SerializeField] [Range(100, 600)]
    private float _x_sens;

    [SerializeField] [Range(100, 600)] private float _y_sens;

    private IInputController _inputController;
    private IPlayerInteractController _interactController;

    public ActorBehaviour CurrentActor => this._actor;

    private void Awake()
    {
        this._inputController = new InputController();
        this._interactController = new PlayerInteractController(this._camera, 3f);
        this._interactController.Enable(true);
    }

    private void Update()
    {
        this._interactController.Update(Time.deltaTime);
        this._actor.ActorController.CharacterController.MoveDirection(this._inputController.Direction());
        this._actor.ActorController.CharacterController.Rotate(this._inputController.MouseMove());

        if (this._inputController.Jump())
        {
            this._actor.ActorController.CharacterController.Jump(1);
        }

        if (this._inputController.Use() && this._interactController.Item != null)
        {
            this._interactController.Item.StartInteract(
                this._actor.ActorController,
                new InteractState()
                {
                    OnStart = () => { Debug.Log("Start"); },
                    OnFinish = () => { Debug.Log("Finish"); },
                    OnCancel = () => { Debug.Log("Cancel"); },
                    OnProcess = (float percent) => { Debug.Log($"Progress {percent}%"); },
                    Time = 0,
                    TimeToEnd = 3f
                }
            );
        }
    }
}