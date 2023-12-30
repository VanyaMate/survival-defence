using Components.Interact;
using Controllers;
using UI.Progress;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private ActorBehaviour _actor;

    [Header("Sens")] [SerializeField] [Range(100, 600)]
    private float _x_sens;

    [SerializeField] [Range(100, 600)] private float _y_sens;
    [Header("camera")] [SerializeField] private Camera _camera;

    [Header("UI")] [SerializeField] private UIBehaviour _uiBehaviour;
    [SerializeField] private UIProgress _uiProgress;

    private IInteractController _interactController;
    private IUIProgressController _uiProgressController;

    public ActorBehaviour CurrentActor => this._actor;

    private void Awake()
    {
        this._interactController = new InteractController(this._camera);
        this._uiProgressController = new UIProgressController(this._uiProgress);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 mouseMoveDirection = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );

        Vector2 moveDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool interact = Input.GetKey(KeyCode.F);
        bool inventory = Input.GetKeyDown(KeyCode.Tab);
        bool close = Input.GetKeyDown(KeyCode.Escape);
        bool uiOpened = this._uiBehaviour.Opened();

        if (inventory)
        {
            this._uiBehaviour.Inventory(true, this._actor.InventoryController);
        }

        if (close)
        {
            if (uiOpened)
            {
                this._uiBehaviour.CloseAll();
            }
            else
            {
                this._uiBehaviour.Menu(true);
            }
        }

        if (!uiOpened)
        {
            if (jump)
            {
                this._actor.Jump();
            }

            if (interact)
            {
                InteractBehaviour interactBehaviour = this._interactController.Interact(this);
                if (interactBehaviour != null)
                {
                    if (interactBehaviour.Progress != 0 && interactBehaviour.Progress != 100)
                    {
                        this._uiProgressController.SetText(interactBehaviour.InteractText);
                        this._uiProgressController.Progress(interactBehaviour.Progress);
                    }
                    else
                    {
                        this._uiProgressController.Hide();
                    }
                }
                else
                {
                    this._uiProgressController.Hide();
                }
            }
            else
            {
                this._uiProgressController.Hide();
            }

            this._interactController.Raycast(2f);
            this._actor.MoveDirection(moveDirection);
            this._actor.Orientation(mouseMoveDirection);
        }
    }
}