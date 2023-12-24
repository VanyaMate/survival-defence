using System;
using Controllers;
using UnityEngine;
using UnityEngine.PlayerLoop;


[RequireComponent(typeof(CharacterController))]
public class ActorBehaviour : MonoBehaviour
{
    [Header("movement")] [SerializeField] private float _movespeed = 3;
    [SerializeField] private float _jumpPower = 6;

    [SerializeField] private Transform _cameraPosition;

    private IMoveController _moveController;
    private IOrientationController _orientationController;

    public IMoveController MoveController => this._moveController;
    public IOrientationController OrientationController => this._orientationController;
    public Transform CameraPosition => this._cameraPosition;

    private void Start()
    {
        this._moveController = new MoveCharacterController(this.GetComponent<CharacterController>());
        this._orientationController = new OrientationController(this._cameraPosition, transform);
    }

    public void Jump()
    {
        this._moveController.Jump(this._jumpPower);
    }

    public void MoveDirection(Vector2 direction)
    {
        this._moveController.MoveDirection(direction * this._movespeed);
    }

    public void Orientation(Vector2 orientation)
    {
        this._orientationController.Rotate(orientation);
    }

    private void Update()
    {
        this._moveController.ChangePosition();
    }
}