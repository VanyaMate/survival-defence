using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Controllers.Input
{
    public interface IInputController
    {
        Vector2 MouseMove();
        Vector2 Direction();
        bool Jump();
        bool OpenMenu();
        bool OpenInventory();
        bool Use();
        bool Close();
    }

    public class InputController : IInputController
    {
        public InputController ()
        {
        }

        public Vector2 MouseMove()
        {
            Vector2 mouseMoveDirection = new Vector2(
                UnityEngine.Input.GetAxisRaw("Mouse X"),
                UnityEngine.Input.GetAxisRaw("Mouse Y")
            );

            return mouseMoveDirection;
        }

        public Vector2 Direction()
        {
            Vector2 moveDirection = new Vector2(
                UnityEngine.Input.GetAxisRaw("Horizontal"),
                UnityEngine.Input.GetAxisRaw("Vertical")
            );

            return moveDirection;
        }

        public bool Jump()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space);
        }

        public bool OpenMenu()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Escape);
        }

        public bool OpenInventory()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Tab);
        }

        public bool Use()
        {
            return UnityEngine.Input.GetKey(KeyCode.F);
        }

        public bool Close()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Escape);
        }
    }
}